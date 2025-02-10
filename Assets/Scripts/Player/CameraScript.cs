using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Movement movement;
    [SerializeField] float DirectionOffset = 5f;
    Vector2 dir;

    private void Update()
    {
        dir = movement.Directon.normalized * DirectionOffset;
        Vector3 direction = dir - new Vector2(Player.position.x, Player.position.y);

        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.position.x - dir.x, Player.position.y - dir.y, -10f), 10f * Time.deltaTime);
    }
}
