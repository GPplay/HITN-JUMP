using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
   public static AudioManager obj;

    public AudioClip jump;
    public AudioClip coin;
    public AudioClip IU;
    public AudioClip hit;
    public AudioClip enemyHit;
    public AudioClip win;

    private AudioSource audioSrc;

    private void Awake()
    {
        obj = this;
        audioSrc = gameObject.AddComponent<AudioSource>();
    }
    public void PlayJump(){ playSound(jump);}
    public void PlayCoin(){ playSound(coin);}
    public void PlayIU() { playSound(IU); }
    public void PlayHit() { playSound(hit); }
    public void PlayEnemyHit() { playSound(enemyHit); }
    public void PlayWin() { playSound(win); }

    public void playSound(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }

    private void OnDestroy()
    {
        obj = null;
    }
}
