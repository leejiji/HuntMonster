using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PathRequestManager : MonoBehaviour
{
}

public class PathRequest {
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Node[] path;
    public Action<Node[]> callback;
    public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Node[]> callback) {
        this.pathStart = pathStart;
        this.pathEnd = pathEnd;
        this.callback = callback;
    }
}
