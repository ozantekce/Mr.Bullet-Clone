using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip death;

    private Rigidbody2D rigidbody;
    void Start()
    {
        alive = true;
        rigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        
    }

    private bool alive;
    void Death()
    {
        alive=false;
        gameObject.tag = "Untagged";
        SoundManager.instance.PlaySoundFX(death, 0.75f);
        FindObjectOfType<GameManager>().CheckEnemyCount();

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
            if (alive)
            {
                Death();
            }

            rigidbody.AddForce(new Vector2(direction.x >0 ? 2:-2,direction.y >0? 0.3f:-0.3f)
                ,ForceMode2D.Impulse);

        }

        if (collision.tag == "Plank"|| collision.tag == "BoxPlank")
        {
            if (alive &&collision.GetComponent<Rigidbody2D>().velocity.magnitude > 1.5f)
            {

                print("Death");
                Death();
            }
        }

        if (collision.tag == "Ground" )
        {
            if (alive &&GetComponent<Rigidbody2D>().velocity.magnitude > 2)
            {
                print("Death");
                Death();
            }
        }

    }


}
