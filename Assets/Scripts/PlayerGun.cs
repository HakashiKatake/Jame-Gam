using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] float Firerate;
    bool canShoot = true;
    [Space]
    [SerializeField] Transform[] ShootPositions;
    [SerializeField] GameObject Bullet;
    [SerializeField] AudioSource _Audio;

    private void Update()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            for (int i = 0; i < ShootPositions.Length; i++)
            {
                Instantiate(Bullet, ShootPositions[i].position, ShootPositions[i].rotation);
            }

            _Audio.Play();

            Invoke(nameof(CanShootTrue), Firerate);
            canShoot = false;
        }
    }

    void CanShootTrue()
    {
        canShoot = true;
    }
}
