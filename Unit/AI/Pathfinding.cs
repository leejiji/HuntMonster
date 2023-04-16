using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
public static class Pathfinding
{
    public static Node[] FindPath(Map map, Transform finder,Transform target) {

        Node StartNode = map.GetNodeFromWorldposition(finder.position);
        Node EndNode = map.GetNodeFromWorldposition(target.position);
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

                Node[] neighborNodes = map.GetNeighborTiles(node.X, node.Y, 1);
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
}