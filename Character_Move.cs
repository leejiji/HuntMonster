using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour
{
    //컨트롤러에서 받은 방향 값으로 이동 담당

    [SerializeField, Range(1f, 5f)] private float speed;

    public void Move(Vector2 inputDir)
    {
        Vector3 moveDir = new Vector3(inputDir.x, 0f, inputDir.y);

        transform.position += moveDir * Time.deltaTime * speed;
    }
}
