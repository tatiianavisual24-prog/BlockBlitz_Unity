using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        audioSource.Play();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, audioSource.clip.length);
    }
}
