using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float Damage;
    [Space]
    [SerializeField] float CollTime;
    [SerializeField] Collider2D Coll;

    private void Start()
    {
        Invoke(nameof(DisableCollider), CollTime);
    }

    void DisableCollider()
    {
        Coll.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Health>(out Health health))
            health.Dammage(Damage);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
