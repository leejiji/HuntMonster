using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Move : MonoBehaviour
{
    //��Ʈ�ѷ����� ���� ���� ������ �̵� ���

    [SerializeField, Range(1f, 5f)] private float speed;

    public void Move(Vector2 inputDir)
    {
        Vector3 moveDir = new Vector3(inputDir.x, 0f, inputDir.y);

        transform.position += moveDir * Time.deltaTime * speed;
    }
}
