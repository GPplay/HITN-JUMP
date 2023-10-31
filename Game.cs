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
    }

    public void AddScore(int recibirScore)
    {
        score = score + recibirScore;
    }

    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
