using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int enemyCount = 1;
    
    private bool gameOver;

    public int blackBullets = 3;
    public int goldenBullets = 1;

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public int EnemyCount { get => enemyCount; set => enemyCount = value; }

    private PlayerController playerController;

    public GameObject blackBullet, goldenBullet;

    private GameObject bulletsUI;


    

    private void Awake()
    {



        playerController = FindObjectOfType<PlayerController>();
        playerController.Ammo = blackBullets + goldenBullets;
        bulletsUI = GameObject.Find("Bullets");
        for (int i = 0; i <blackBullets; i++)
        {
            GameObject tempBlackBullet = Instantiate(blackBullet);
            tempBlackBullet.transform.SetParent(bulletsUI.transform);
        }
        for (int i = 0; i < goldenBullets; i++)
        {
            GameObject tempGoldenBullet = Instantiate(goldenBullet);
            tempGoldenBullet.transform.SetParent(bulletsUI.transform);
        }

    }

    void Update()
    {
        
        if(!gameOver && playerController.Ammo<=0 && enemyCount>0 
            && GameObject.FindGameObjectsWithTag("Bullet").Length<=0)
        {
            gameOver = true;
            GameUI.instance.GameOVerScreen();
        }

    }


    public void CheckBullets()
    {
        if(goldenBullets > 0)
        {
            goldenBullets--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }else if(blackBullets > 0)
        {
            blackBullets--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }

    }

    public void CheckEnemyCount()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount <= 0)
        {
            GameUI.instance.WinScreen();
        }

    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
