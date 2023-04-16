using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
using DG.Tweening;
public class PathfindingManager : SingletonBehaviour<PathfindingManager>
{
    [SerializeField,  Range(1,20)] int ThreadNum;
    List<PathFindingThread> PathFindingThreadList = new List<PathFindingThread>();

    public List<MaterialPropertyBlock> block;
    static Queue<PathRequest> results = new Queue<PathRequest>();
    Queue<PathFindingThread> PathFindThreadQueue;
    
    bool SE = true;
    protected override void Awake() {
        base.Awake();
        DOTween.SetTweensCapacity(500, 50);
        //MapList = new Map MapManager.Instance.m_Map;
        for(int i = 0; i < ThreadNum; i++) {
            Map mapData = new Map(MapManager.Instance.m_Map);
            PathFindingThread pathFindingThread = new PathFindingThread(mapData);
            PathFindingThreadList.Add(pathFindingThread);
        }
        EventManager<SceneEvent>.Instance.AddListener(SceneEvent.SceneChangeStart, this, (eventType, sender, param) => { SE = false; });
    }
    private void Update() {
        if(SE)
        ThreadSearch();
    }
    public void OneThread() {
        if (results.Count > 0) {
            int resultCount = results.Count;

            for (int i = 0; i < resultCount; i++) {
                if (SE) {
                    Debug.Log("1스레드 시작");
                    SE = false;
                    PathRequest PathResult = results.Dequeue();
                    Thread thread = new Thread(() => {
                        PathResult.path = FindPath(MapManager.Instance.m_Map, PathResult.pathStart, PathResult.pathEnd);
                        PathResult.callback(PathResult.path);
                        SE = true;
                    });
                    thread.Start();
                }
                else
                    return;
            }

        }
    }
    public void ThreadSearch() {
        if (results.Count > 0) {
            int resultCount = results.Count;
            for (int i = 0; i < resultCount; i++) {
                //Debug.Log(resultCount);
                for (int k = 0; k < PathFindingThreadList.Count; k++) {
                    if (PathFindingThreadList[k].SearchEnd) {
                        Debug.Log(k + "번째 스레드 시작");
                        PathFindingThreadList[k].SearchEnd = false;
                        PathRequest PathResult = results.Dequeue();
                        Thread thread = new Thread(() => {
                            PathResult.path = FindPath(PathFindingThreadList[k].MapData, PathResult.pathStart, PathResult.pathEnd);
                            PathResult.callback(PathResult.path);
                            PathFindingThreadList[k].SearchEnd = true;
                        });
                        thread.Start();
                        break;
                    }
                }
            }
        }
    }
    public void PathFind(Vector3 finder, Vector3 target, Action<Node[]> callback) {
        PathRequest result = new PathRequest(finder, target, callback);
        PathFind(result);
    }
    void PathFind(PathRequest result) {
        lock (result) {
            results.Enqueue(result);
        }
    }
    public Node[] FindPath(Map mapData, Vector3 finder,Vector3 target) {

        Node StartNode = mapData.GetNodeFromWorldposition(finder);
        Node EndNode = mapData.GetNodeFromWorldposition(target);
        if (StartNode.IsWalkable && StartNode != null && EndNode != null && EndNode.IsWalkable) {
            List<Node> Way = new List<Node>();
            Heap<Node> OpenList = new Heap<Node>(MapManager.Instance.MapXSize * MapManager.Instance.MapYSize);
            HashSet<Node> CloseList = new HashSet<Node>();
            StartNode.parent = null;
            OpenList.Add(StartNode);
            int asd = 0;
            while (OpenList.Count > 0) {
                asd++;
                if (asd > 100000) {
                    Debug.Log("초과");
                    return null;
                }
                Node node = OpenList.RemoveFirst();
                CloseList.Add(node);
                if (node.WorldPosition == EndNode.WorldPosition) {
                    Debug.Log("찾음");
                    int asaa = 0;
                    while (node.parent != null) {
                        asaa++;
                        if (asaa > 10000) {
                            Debug.Log("부모 초과");
                            return null;
                        }
                        Way.Add(node);
                        node = node.parent;
                    }
                    Way.Add(StartNode);
                    Node[] ways = Way.ToArray();
                    Array.Reverse(ways);
                    return ways;
                }

                Node[] neighborNodes = mapData.GetNeighborTiles(node.X, node.Y, 1);
                // 탐색 노드의 주변 노드 탐색
                for (int i = 0; i < neighborNodes.Length; i++) {
                    Node neighborNode = neighborNodes[i];
                    if (!neighborNode.IsWalkable || CloseList.Contains(neighborNode))
                        continue;
                    int nextGCost = node.G + GetDistance(node, neighborNode);
                    if (nextGCost < neighborNode.G || !OpenList.Contains(neighborNode)) {
                        neighborNode.H = GetDistance(neighborNode, EndNode);
                        neighborNode.G = nextGCost + neighborNode.Penalty;
                        neighborNode.parent = node;
                        if (!OpenList.Contains(neighborNode)) {
                            OpenList.Add(neighborNode);
                        }
                        else {
                            OpenList.UpdateItem(neighborNode);
                        }
                    }
                }
            }
        }
        return null;
    }

    public static int GetDistance(Node tile1, Node tile2) {
        float x = Mathf.Abs(tile1.X - tile2.X);
        float y = Mathf.Abs(tile1.Y - tile2.Y);
        if (x > y)
            return (int)(14 * y + 10 * (x - y));
        else
            return (int)(14 * x + 10 * (y - x));
    }

    public class PathFindingThread {
        public Map MapData;
        public bool SearchEnd;
        public PathFindingThread(Map mapData) {
            MapData = mapData;
            SearchEnd = true;
        }
    }
}