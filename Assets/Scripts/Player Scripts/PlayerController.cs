using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed = 100, bulletSpeed = 100;
    [SerializeField]
    private int ammo = 4;

    [SerializeField]
    private Transform handPos,firePos1,firePos2;
    [SerializeField]
    private LineRenderer laser;

    public GameObject bullet;

    public int Ammo { get => ammo; set => ammo = value; }


    [SerializeField]
    private GameObject crosshair;

    public AudioClip gunShot;

    void Awake()
    {

        crosshair = GameObject.Find("Crosshair");
        crosshair.SetActive(false);

        laser.enabled = false;

    }

    void Update()
    {

        if (!IsMouseOverUI())
        {
            if (Input.GetMouseButton(0))
            {
                Aim();
            }


            if (Input.GetMouseButtonUp(0))
            {
                if (ammo > 0)
                    Shoot();
                else
                {
                    laser.enabled = false;
                    crosshair.SetActive(false);
                }
            }

        }



    }



    void Aim()
    {
        crosshair.SetActive(true);
        crosshair.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward *10);

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - handPos.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        handPos.rotation = Quaternion.Slerp(handPos.rotation, rotation, rotateSpeed * Time.deltaTime);

        laser.enabled = true;
        laser.SetPosition(0,firePos1.position);
        firePos2.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        laser.SetPosition(1,firePos2.position);


    }


    void Shoot()
    {
        crosshair.SetActive(false);
        laser.enabled=false;
        GameObject tempBullet = Instantiate(bullet,firePos1.position,Quaternion.identity);
        Rigidbody2D rigidbody = tempBullet.GetComponent<Rigidbody2D>();

        if (transform.localScale.x > 0)
        {
            rigidbody.AddForce(firePos1.right * bulletSpeed,ForceMode2D.Impulse);
        }
        else
        {
            rigidbody.AddForce(-firePos1.right * bulletSpeed, ForceMode2D.Impulse);
        }

        ammo--;
        FindObjectOfType<GameManager>().CheckBullets();
        SoundManager.instance.PlaySoundFX(gunShot, 0.3f);

        Destroy(tempBullet, 2);

    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
