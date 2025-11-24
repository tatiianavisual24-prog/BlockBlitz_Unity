using UnityEngine;

public class StartSpinPop : MonoBehaviour
{
    public float startScale = 0.2f;        // tamaño pequeño inicial
    public float finalScale = 1f;          // tamaño final
    public float spinAngle = 90f;          // gira de 12 a 4 (90 grados)
    public float spinTime = 0.35f;         // tiempo de giro
    public float popTime = 0.15f;          // rebote final
    public float stayTime = 1.5f;          // tiempo visible

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();

        // posición inicial: un poquito arriba
        rt.anchoredPosition += new Vector2(0, 200);

        // tamaño inicial pequeño
        rt.localScale = Vector3.one * startScale;

        // rotación inicial
        rt.rotation = Quaternion.Euler(0, 0, 0);

        // animación principal
        StartCoroutine(PlayAnimation());
    }

    private System.Collections.IEnumerator PlayAnimation()
    {
        // 1️⃣ Giro + crecer al mismo tiempo
        float t = 0;
        while (t < spinTime)
        {
            t += Time.deltaTime;
            float lerp = t / spinTime;

            // giro
            float angle = Mathf.Lerp(0, spinAngle, lerp);
            rt.rotation = Quaternion.Euler(0, 0, angle);

            // tamaño
            float scale = Mathf.Lerp(startScale, finalScale, lerp);
            rt.localScale = Vector3.one * scale;

            yield return null;
        }

        // 2️⃣ POP (rebote)
        t = 0;
        Vector3 overshoot = Vector3.one * (finalScale * 1.15f);
        while (t < popTime)
        {
            t += Time.deltaTime;
            float lerp = t / popTime;

            rt.localScale = Vector3.Lerp(Vector3.one * finalScale, overshoot, lerp);
            yield return null;
        }

        // vuelve al tamaño exacto
        rt.localScale = Vector3.one * finalScale;

        // 3️⃣ queda un rato
        yield return new WaitForSeconds(stayTime);

        // 4️⃣ se apaga
        gameObject.SetActive(false);
    }
}
