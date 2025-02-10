using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform SpriteObject;
    [Space]
    public float Speed;
    [SerializeField] float RotationSpeed;
    Vector3 mousePos;
    Camera cam;
    public Vector3 Directon;
    float smoothRot;

    private void Start()
    {
        cam = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float MouseX = Input.GetAxisRaw("Mouse X");
        smoothRot = Mathf.Lerp(smoothRot, -MouseX * RotationSpeed, 10f * Time.deltaTime);
        transform.Rotate(0, 0, smoothRot);

        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        Directon = transform.up * Vertical + transform.right * Horizontal;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Directon.normalized * Speed, ForceMode2D.Force);
    }
}
