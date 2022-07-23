using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{

    public GameObject explosionPrefab;

    [SerializeField]
    private float radius=1, power = 10;


    private void Explode()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);

        GameObject exp = Instantiate(explosionPrefab);
        exp.transform.position = explosionPos;
        Destroy(exp,0.8f);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                
                Vector2 explodeDir = rb.position - explosionPos;

                rb.gravityScale = 1;
                rb.AddForce(power *explodeDir, ForceMode2D.Impulse);

            }

            if (collider.CompareTag("Enemy"))
            {
                collider.tag = "Untagged";
            }

        }

    }


    /*
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);

    }
    */


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explode();
            Destroy(gameObject);
        }

    }

}
