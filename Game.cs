using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;

    public int vidaMax = 3;

    public bool paused = false;
    public int score = 0;
    public int nivel = 1;

    private void Awake()
    {
        obj = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        UImanager.obj.StartGame();
        UImanager.obj.Menupausa.SetActive(false);
        nivel++;
    }

    public void AddScore(int recibirScore)
    {
        score = score + recibirScore;
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SiguienteNivel()
    {
        if(nivel <= 5) {
            SceneManager.LoadScene(nivel);
        }
        else
        {
            nivel = 0;
            SceneManager.LoadScene(nivel);
        }
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
