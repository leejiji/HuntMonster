using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CompositeShape
{
    public static bool triangulate(Vector2[] vertices, out int[] triangles) {
        triangles = null;
        if (vertices == null) {
            Debug.Log("버텍스 없다");
            return false;
        }
        if (vertices .Length < 3) {
            Debug.Log("버텍스 적다");
            return false;
        }
        if (vertices.Length > 1024) {
            Debug.Log("버텍스 많다");
            return false;
        }
        List<int> indexList = new List<int>();
        for(int i = 0; i < vertices.Length; i++) {
            indexList.Add(i);
        }
        // 삼각형 최대 갯수는 정점 - 2 개로 고정
        int totalTriangleCount = vertices.Length - 2;
        int totalTriangleIndexCount= totalTriangleCount * 3;

        triangles = new int[totalTriangleIndexCount];
        int triangleIndexCount = 0;
        int asd = 0; 
        while (indexList.Count > 3) {
            asd++;
            if (asd > 1000)
                break;
            for(int i = 0; i< indexList.Count; i++) {
                int a = indexList[i];
                int b = GetItem(indexList, i - 1);
                int c = GetItem(indexList, i + 1);

                Vector2 va = vertices[a];
                Vector2 vb = vertices[b];
                Vector2 vc = vertices[c];

                Vector2 vatovb = vb - va;
                Vector2 vatovc = vc - va;

                if(cross(vatovb, vatovc) < 0f) {
                    continue;
                }

                bool isEar = true;
                for(int j = 0; j < vertices.Length; j++) {
                    if(j == a || j == b || j == c) {
                        continue;
                    }

                    Vector2 p = vertices[j];

                    if(isPointInTriangle(p, vb, va, vc)) {
                        isEar = false;
                        break;
                    }
                }
                if (isEar) {
                    triangles[triangleIndexCount++] = b;
                    triangles[triangleIndexCount++] = a;
                    triangles[triangleIndexCount++] = c;

                    indexList.RemoveAt(i);
                    break;
                }
            }
        }
        triangles[triangleIndexCount++] = indexList[0];
        triangles[triangleIndexCount++] = indexList[1];
        triangles[triangleIndexCount++] = indexList[2];
        return true;
    }
    public static bool isPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
        Vector2 ab = b - a;
        Vector2 bc = c - b;
        Vector2 ca = a - c;

        Vector2 ap = p - a;
        Vector2 bp = p - b;
        Vector2 cp = p - c;

        float cross1 = cross(ab, ap);
        float cross2 = cross(bc, bp);
        float cross3 = cross(ca, cp);

        if (cross1 > 0f || cross2 > 0f || cross3 > 0f)
            return false;
        return true;
    }
    // array 갯수 넘는거 계산해서 주기
    static int GetItem(List<int> array, int index) {
        if(index >= array.Count) {
            return array[index % array.Count];
        }
        else if(index < 0) {
            return array[index % array.Count + array.Count];
        }
        else {
            return array[index];
        }
    }
    static float cross(Vector2 a, Vector2 b){
        return a.x * b.y - a.y * b.x;
    }
}
