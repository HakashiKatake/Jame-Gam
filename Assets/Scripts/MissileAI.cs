using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MissileAI : MonoBehaviour
{
    Rigidbody2D rb;
    Transform player;
    [SerializeField] float RotationSpeed;
    [SerializeField] float Speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 VectorToTarget = player.position - transform.position;
        float angle = Mathf.Atan2(VectorToTarget.y, VectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion r = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, r, RotationSpeed * Time.deltaTime); ;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * Speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GetComponent<Health>().Dammage(999f);
        }
    }
}
