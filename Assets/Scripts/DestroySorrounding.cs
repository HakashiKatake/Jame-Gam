using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySorrounding : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Road" || collision.tag == "Building")
            Destroy(collision.gameObject);
    }
}
