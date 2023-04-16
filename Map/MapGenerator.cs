using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {
    public Node[,] MapData;
    Vector3 center;
    public Vector3 Center => center;
    float nodeSize;
    public float NodeSize => nodeSize;

    int MapNodeXCount;
    int MapNodeYCount;

    Vector3 mapCorner;
    public Vector3 MapCorner => mapCorner;
    public Map(Node[,] mapData, Vector3 center, int mapWidth, int mapHeight, float nodeSize) {
        MapData = mapData;
        MapNodeXCount = Mathf.RoundToInt(mapWidth / nodeSize);
        MapNodeYCount = Mathf.RoundToInt(mapHeight / nodeSize);
        this.center = center;
        this.nodeSize = nodeSize;

        Vector3 LeftDownCorner = center - new Vector3(mapWidth * 0.5f, 0, mapHeight * 0.5f);
        mapCorner = LeftDownCorner;
    }
    public Map(Map map) {
        this.center = map.center;
        Node[,] cloneMapData = map.MapData;
        int X = cloneMapData.GetLength(0);
        int Y = cloneMapData.GetLength(1);
        this.MapData = new Node[X, Y];
        for (int x = 0; x < X; x++) {
            for (int y = 0; y < Y; y++) {
                MapData[x, y] = new Node(cloneMapData[x, y]);
            }
        }
        Debug.Log(cloneMapData[0, 0] == MapData[0, 0]);
        this.mapCorner = map.mapCorner;
        this.nodeSize = map.nodeSize;
        this.MapNodeXCount = map.MapNodeXCount;
        this.MapNodeYCount = map.MapNodeYCount;
    }
    public Node[] GetNeighborTiles(int tileX, int tileY, int range) {
        List<Node> tileList = new List<Node>();
        for (int x = tileX - range; x <= tileX + range; x++) {
            for (int y = tileY - range; y <= tileY + range; y++) {
                if (x == tileX && y == tileY)
                    continue;

                if (0 <= x && x < MapNodeXCount && 0 <= y && y < MapNodeYCount) {
                    tileList.Add(MapData[x, y]);
                }
            }
        }
        return tileList.ToArray();
    }
    public Node GetNodeFromWorldposition(Vector3 worldPosition) {
        float percentX = ((worldPosition.x - MapCorner.x) / NodeSize / MapNodeXCount);
        float percentY = ((worldPosition.z - MapCorner.z) / NodeSize / MapNodeYCount);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.RoundToInt(MapNodeXCount * percentX);
        int y = Mathf.RoundToInt(MapNodeYCount * percentY);
        if (isPositionInMapNodes(x, y)) {
            Node node = MapData[x, y];
            return node;
        }
        return null;
    }
    public bool isPositionInMapNodes(float x, float y) {
        if (0 <= x * NodeSize && x * NodeSize < MapNodeXCount && 0 <= y * NodeSize && y * NodeSize < MapNodeYCount)
            return true;
        return false;
    }
}

public class MapGenerator
{

    public Map MapGenerate(Vector3 center, int mapWidth, int mapHeight, float nodeSize) {
        Vector3 LeftDownCorner = center - new Vector3(mapWidth * 0.5f, 0, mapHeight * 0.5f);
        Vector3 MapCorner = LeftDownCorner;

        int MapNodeXCount = Mathf.RoundToInt(mapWidth / nodeSize);
        int MapNodeYCount = Mathf.RoundToInt(mapHeight / nodeSize);       
        Node[,] MapData = new Node[MapNodeXCount, MapNodeYCount];
        Map map = new Map(MapData, center, mapWidth, mapHeight, nodeSize);
        int layermask = 1 << LayerMask.NameToLayer("Object");

        float posinCell = 0.5f * nodeSize;
        List<Node> cantWalkableNode = new List<Node>();
        for (int y = 0; y < MapNodeYCount; y++) {
            for (int x = 0; x < MapNodeXCount; x++) {
                float NodeX = x * nodeSize + MapCorner.x;
                float NodeY = y * nodeSize + MapCorner.z;
                Vector3 worldPosition = new Vector3(NodeX + posinCell, 0, NodeY + posinCell);
                RaycastHit ray;
                if (Physics.Raycast(new Vector3(NodeX + posinCell, 10, NodeY + posinCell), Vector3.down, out ray, 50, layermask)) {
                    if (!ray.collider.isTrigger) {
                        MapData[x, y] = new Node(x, y, worldPosition, 0, false);
                        cantWalkableNode.Add(MapData[x, y]);
                    }
                    else {
                        MapData[x, y] = new Node(x, y, worldPosition, 0, true);
                    }
                }
                else {
                    MapData[x, y] = new Node(x, y, worldPosition, 0, true);
                }
            }
        }
        for (int i = 0; i < cantWalkableNode.Count; i++) {
            Node[] neighborNodes = map.GetNeighborTiles(cantWalkableNode[i].X, cantWalkableNode[i].Y, 2);
            for (int k = 0; k < neighborNodes.Length; k++) {
                neighborNodes[k].Penalty += 5;
            }
        }
        Map map2 = new Map(MapData, center, mapWidth, mapHeight, nodeSize);
        return map2;
    }
}
