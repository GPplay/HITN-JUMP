using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreGive =100;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Game.obj.AddScore(scoreGive);

            AudioManager.obj.PlayCoin();
            UImanager.obj.UpdateScore();

            FXmanager.obj.verPop(transform.position);
            gameObject.SetActive(false);
        }
    }
}
