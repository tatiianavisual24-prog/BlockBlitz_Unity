using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Movimiento al salir del bloque")]
    public float riseHeight = 1f;      
    public float riseSpeed = 6f;      
    public float visibleTime = 0.25f;  
    [Header("Sonido")]
    public AudioClip coinClip;         

    private Vector3 startPos;
    private Vector3 upPos;
    private AudioSource audioSource;

    private void Awake()
    {
       
        audioSource = GetComponent<AudioSource>();

      
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

  
        audioSource.spatialBlend = 0f;
        audioSource.volume = 1f;
    }

    private void Start()
    {
        startPos = transform.position;
        upPos = startPos + Vector3.up * riseHeight;

 
        if (audioSource.clip == null && coinClip != null)
        {
            audioSource.clip = coinClip;
        }

  
        if (audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        else
        {
            Debug.LogWarning("Coin: no hay AudioClip asignado en el prefab.", this);
        }

  
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
