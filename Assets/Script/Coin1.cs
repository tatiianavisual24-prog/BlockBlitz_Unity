using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Movimiento al salir del bloque")]
    public float riseHeight = 1f;      // cuánto sube la moneda
    public float riseSpeed = 6f;       // velocidad de subida
    public float visibleTime = 0.25f;  // tiempo que queda arriba antes de desaparecer

    [Header("Sonido")]
    public AudioClip coinClip;         // puedes arrastrar aquí el sonido de moneda

    private Vector3 startPos;
    private Vector3 upPos;
    private AudioSource audioSource;

    private void Awake()
    {
        // Intentar obtener un AudioSource existente
        audioSource = GetComponent<AudioSource>();

        // Si no hay, creamos uno para que nunca falle
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Aseguramos que el audio sea 2D y audible
        audioSource.spatialBlend = 0f;
        audioSource.volume = 1f;
    }

    private void Start()
    {
        startPos = transform.position;
        upPos = startPos + Vector3.up * riseHeight;

        // Si el AudioSource no tiene clip pero coinClip sí, se lo asignamos
        if (audioSource.clip == null && coinClip != null)
        {
            audioSource.clip = coinClip;
        }

        // Reproducir el sonido si hay clip
        if (audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            Debug.LogWarning("Coin: no hay AudioClip asignado en el prefab.", this);
        }

        // Animación de subida + desaparición
        StartCoroutine(RiseAndDisappear());
    }

    private IEnumerator RiseAndDisappear()
    {
        float t = 0f;

        // Subida
        while (t < 1f)
        {
            t += Time.deltaTime * riseSpeed;
            transform.position = Vector3.Lerp(startPos, upPos, t);
            yield return null;
        }

        // Se queda un momento arriba
        yield return new WaitForSeconds(visibleTime);

        // Se destruye sola
        Destroy(gameObject);
    }
}
