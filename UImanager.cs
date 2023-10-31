using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager obj;

    [SerializeField] private GameObject Botonpausa;
    public GameObject Menupausa;
    public GameObject Winner;
    private bool juegoPausado = false;
    private int r=0;

    public Text livesLbl;
    public Text scoreLbl;

    public Transform UIPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                BotonContinuar();
            }
            else
            {
                BotonPausa();
            }
        }
    }

    private void Awake()
    {
        obj = this;
    }

    public void UpdateLives()
    {
        livesLbl.text = "" + Player.obj.vidas;
    }

    public void UpdateScore()
    {
        scoreLbl.text = "" + Game.obj.score;
    }

    public void StartGame()
    {
        AudioManager.obj.PlayIU();
        Game.obj.paused =true;
        UIPanel.gameObject.SetActive(true);
    }

    public void HideInit()
    {
        AudioManager.obj.PlayIU();
        Game.obj.paused =false;
        UIPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        obj = null;
    }

    public void BotonPausa()
    {
        juegoPausado = true;
        Time.timeScale = 0;
        Botonpausa.SetActive(false);
        //un if que sea un comprabante de que si el player no toco la bandera aprezca pero si la toco no aparezca
        if (r==0)
        {
            Menupausa.SetActive(true);
        }
        Game.obj.paused = true;
    }

    public void BotonContinuar()
    {
        Game.obj.paused = false;
        juegoPausado =false;
        Time.timeScale = 1;
        Botonpausa.SetActive(true);
        Menupausa.SetActive(false);
    }

    public void BotonReiniciar()
    {
        Time.timeScale = 1;
        Game.obj.gameOver();
    }

    public void winner()
    {
        r = 1;
        BotonPausa();
        Winner.SetActive(true);
    }
}
