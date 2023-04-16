using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {
    bool isWalkable;
    public bool IsWalkable => isWalkable;
    public int F { get { return G + H; } }
    public int G;
    public int H;
    int x;
    public int X => x;
    int y;
    public int Y => y;

    int heapIndex;
    public int HeapIndex { get => heapIndex; set { heapIndex = value; } }
    public int Penalty;

    public Vector3 WorldPosition;
    public Vector2 WorldVec2Position { get { return new Vector3(WorldPosition.x, WorldPosition.z); } }
    public Node parent;
    public Node(int x, int y, Vector3 worldPosition, int penalty, bool isWalkable) {
        this.isWalkable = isWalkable;
        this.x = x;
        this.y = y;
        Penalty = penalty;
        WorldPosition = worldPosition;
    }
    public Node(Node node) {
        this.isWalkable = node.isWalkable;
        this.x = node.x;
        this.y = node.y;
        this.Penalty = node.Penalty;
        this.WorldPosition = node.WorldPosition;
    }
    // 답에 가까우면 1 아니면 -1
    public int CompareTo(Node other) {
        //Debug.Log("비교 " +WorldPosition + "  "+ F + "  " +other.WorldPosition+"  " +other.F);
        int compare = F.CompareTo(other.F);
        if (compare == 0) {
            compare = H.CompareTo(other.H);
        }
        return -compare;
    }
}