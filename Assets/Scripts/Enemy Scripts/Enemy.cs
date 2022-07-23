using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }


    void Death()
    {
        gameObject.tag = "Untagged";
        foreach(Transform ob in transform)
        {
            ob.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Bullet")
        {
            Vector2 direction = transform.position - collision.transform.position;
            direction.Normalize();
            if (transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale<1)
            {
                Death();
            }

            rigidbody.AddForce(new Vector2(direction.x >0 ? 2:-2,direction.y >0? 0.3f:-0.3f)
                ,ForceMode2D.Impulse);

        }


    }


}
