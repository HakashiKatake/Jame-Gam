using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public Transform Target;

    private void Update()
    {
        Vector3 VectorToTarget = Target.position - transform.position;
        float angle = Mathf.Atan2(VectorToTarget.y, VectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
}
