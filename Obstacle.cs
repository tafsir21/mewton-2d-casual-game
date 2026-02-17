using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    public bool isPoint;
    public bool hasBeenHit = false;


    [SerializeField] private float fallSpeed = 3.5f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        rb.linearVelocity = Vector2.down * fallSpeed;
    }

    public void DestroyObstacle()
    {
        if (hasBeenHit)
        {
            return;
        }

        hasBeenHit = true;
        StartCoroutine(SqueezeAndFade());
    }


    // fruit droping animation
    private IEnumerator SqueezeAndFade()
    {
        rb.linearVelocity = Vector2.zero;
        col.enabled = false;

        float duration = 0.5f;
        float time = 0f;

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x * 1.3f, originalScale.y * 0.1f, originalScale.z);

        Color originalColor = sr.color;

        while (time < duration)
        {
            float t = time / duration;

            // effect 
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1 - t);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
