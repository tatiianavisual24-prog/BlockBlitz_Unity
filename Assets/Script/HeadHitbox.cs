using UnityEngine;
using StarterAssets;  

public class HeadHitbox : MonoBehaviour
{
    private ThirdPersonController controller;

    private void Awake()
    {
        // Encuentra el ThirdPersonController en el padre
        controller = GetComponentInParent<ThirdPersonController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HeadHitbox golpeó con: " + other.name);

        // Buscar FallingCube en el objeto golpeado
        FallingCube cube = other.GetComponent<FallingCube>();
        if (cube == null)
            cube = other.GetComponentInParent<FallingCube>();

        if (cube != null)
        {
            cube.Hit();

            // Cortar el salto
            if (controller != null)
                controller.StopJumpOnHeadHit();
        }
    }
}
