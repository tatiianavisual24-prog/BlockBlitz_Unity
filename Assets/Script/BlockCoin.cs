using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlockCoin : MonoBehaviour
{
    [Header("Movimiento al salir del bloque")]
    public float riseHeight = 1f;
    public float riseSpeed = 6f;
    public float visibleTime = 0.25f;

    private Vector3 startPos;
    private Vector3 upPos;
    private AudioSource audioSource;

    private void Start()
    {
        startPos = transform.position;
        upPos = startPos + Vector3.up * riseHeight;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

        StartCoroutine(RiseAndDisappear());
    }

    private IEnumerator RiseAndDisappear()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * riseSpeed;
            transform.position = Vector3.Lerp(startPos, upPos, t);
            yield return null;
        }

        yield return new WaitForSeconds(visibleTime);

        Destroy(gameObject);
    }
}
