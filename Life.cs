using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int scoreGive = 30;
    public int life = 1;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Game.obj.AddScore(scoreGive);
            Player.obj.addLives(life);
            UImanager.obj.UpdateLives();
            UImanager.obj.UpdateScore();
            AudioManager.obj.PlayCoin();
            FXmanager.obj.verPop(transform.position);
            gameObject.SetActive(false);
        }

    }
}
