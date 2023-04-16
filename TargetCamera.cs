using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    //카메라 움직임 담당

    [SerializeField] private GameObject target;

    void Update()
    {
        Vector3 FixedPos = new Vector3(
            target.transform.position.x + 0f,
            target.transform.position.y + 21f,
            target.transform.position.z + -20f);

        transform.position = FixedPos;
    }
}
