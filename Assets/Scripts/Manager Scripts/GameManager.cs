using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int enemyCount = 1;
    
    private bool gameOver;

    public bool GameOver { get => gameOver; set => gameOver = value; }
    public int EnemyCount { get => enemyCount; set => enemyCount = value; }

    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    void Update()
    {
        
        if(!gameOver && playerController.Ammo<=0 && enemyCount>0)
        {
            print("GameOver");
            gameOver = true;
        }

    }

    public void CheckEnemyCount()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount <= 0)
        {
            print("Win");
        }

    }


}
