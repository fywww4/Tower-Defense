using UnityEngine;
using System.Collections;

public class BombBehavior : MonoBehaviour
{
    public float explosionDelay = 2f;

    public float explosionRadius = 2f;

    void Start()
    {
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        // 找附近敵人
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                explosionRadius
            );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                HealthBar hp =
                    hit.transform.Find("HealthBar")
                    .GetComponent<HealthBar>();

                // 扣除50%生命
                float damage = hp.maxHealth * 0.5f;

                hp.currentHealth -= damage;

                // 敵人死亡
                if (hp.currentHealth <= 0)
                {
                    Destroy(hit.gameObject);
                }
            }
        }

        Destroy(gameObject);
    }

    // 顯示爆炸範圍
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            explosionRadius
        );
    }
}
