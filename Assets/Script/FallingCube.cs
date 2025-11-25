using System.Collections;
using UnityEngine;

public class FallingCube : MonoBehaviour
{
    [Header("Caída inicial")]
    public float startHeight = 6f;
    public float fallSpeed = 15f;
    public float delay = 0f;

    [Header("Bump al golpear")]
    public float bumpHeight = 0.3f;
    public float bumpSpeed = 8f;

    [Header("Golpes para romper")]
    public int maxHits = 3;

    [Header("FX")]
    public GameObject poofFX;   // Prefab del efecto

    [Header("Monedas")]
    public GameObject coinPrefab;   // ← arrastras aquí el prefab de la moneda
    public int coinsToSpawn = 3;    // ← cuántas monedas saldrán del bloque
    public float coinSpawnHeight = 1.2f; // ← qué tan arriba del bloque salen

    // Estado interno
    private Vector3 target;
    private bool isFalling = false;
    private bool hasLanded = false;

    private bool isBumping = false;
    private bool isBroken = false;
    private int hitCount = 0;

    private int coinsSpawned = 0;   // ← cuántas monedas ya salieron

    void Start()
    {
        target = transform.position;

        transform.position = new Vector3(
            target.x,
            target.y + startHeight,
            target.z
        );

        Invoke(nameof(StartFalling), delay);
    }

    void StartFalling()
    {
        isFalling = true;
    }

    void Update()
    {
        if (isFalling)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                fallSpeed * Time.deltaTime
            );

            if (!hasLanded && Vector3.Distance(transform.position, target) < 0.01f)
            {
                hasLanded = true;
                isFalling = false;

                if (StartTextManager.Instance != null)
                {
                    StartTextManager.Instance.CubeLanded();
                }
            }
        }
    }

    // Llamado por HeadHitbox cuando Mario golpea el cubo
    public void Hit()
    {
        if (isFalling || !hasLanded || isBumping || isBroken)
            return;

        // ---- FX: brillo arriba del cubo ----
        if (poofFX != null)
        {
            Instantiate(
                poofFX,
                transform.position + Vector3.up * 0.6f,
                Quaternion.identity
            );
        }

        // Contar golpe
        hitCount++;

        Debug.Log($"Golpe {hitCount}/{maxHits} en {gameObject.name}");

        // ---- MONEDA: sacar hasta 3 veces ----
        if (coinPrefab != null && coinsSpawned < coinsToSpawn)
        {
            Vector3 spawnPos = transform.position + Vector3.up * coinSpawnHeight;
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            coinsSpawned++;
        }

        if (hitCount >= maxHits)
        {
            isBroken = true;
        }

        StartCoroutine(Bump());
    }

    private IEnumerator Bump()
    {
        isBumping = true;

        Vector3 basePos = transform.position;
        Vector3 upPos = basePos + Vector3.up * bumpHeight;

        float t = 0f;

        // Subir
        while (t < 1f)
        {
            t += Time.deltaTime * bumpSpeed;
            transform.position = Vector3.Lerp(basePos, upPos, t);
            yield return null;
        }

        t = 0f;

        // Bajar
        while (t < 1f)
        {
            t += Time.deltaTime * bumpSpeed;
            transform.position = Vector3.Lerp(upPos, basePos, t);
            yield return null;
        }

        transform.position = basePos;
        isBumping = false;

        if (isBroken)
        {
            if (StartTextManager.Instance != null)
            {
                StartTextManager.Instance.BlockDestroyed();
            }

            Destroy(gameObject);
        }
    }
}