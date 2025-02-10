using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float Damage;

    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.TryGetComponent<Health>(out Health health))
        {
            health.Dammage(Damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
      Destroy(gameObject);
    }
}
