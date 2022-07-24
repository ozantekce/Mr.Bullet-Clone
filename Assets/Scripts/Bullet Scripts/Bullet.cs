using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public AudioClip boxHit, plankHit, groundHit, explodeHit;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Box"))
        {
            SoundManager.instance.PlaySoundFX(boxHit, 1f);
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlaySoundFX(groundHit, 1f);
        }


        if (collision.gameObject.CompareTag("Plank"))
        {
            SoundManager.instance.PlaySoundFX(plankHit, 1f);
        }

        if (collision.gameObject.CompareTag("Tnt"))
        {
            SoundManager.instance.PlaySoundFX(explodeHit, 1f);
        }

    }

}
