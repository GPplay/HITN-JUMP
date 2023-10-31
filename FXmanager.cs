using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXmanager : MonoBehaviour
{
    public static FXmanager obj;

    public GameObject pop;

    private void Awake()
    {
        obj = this;
    }

    public void verPop(Vector3 pos)
    {
        pop.gameObject.GetComponent<Pop>().aparecer(pos);
    }

    private void OnDestroy()
    {
        obj = null;
    }

}
