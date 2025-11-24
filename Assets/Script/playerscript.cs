using UnityEngine;

public class playerscript : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed = 5f;
    public float sideForce = 5f;

    [HideInInspector]
    public bool canMove = true;   // El manager lo pondrá en false al inicio

    void Update()
    {
        if (!canMove)
            return; // 🔒 Si está bloqueado, no hace nada

        // Hacia adelante
        rb.AddForce(new Vector3(0, 0, forwardSpeed) * Time.deltaTime);

        // Izquierda
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector3(-sideForce, 0, 0) * Time.deltaTime);

        // Derecha
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector3(sideForce, 0, 0) * Time.deltaTime);
    }
}
