using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public string Transtiion;
    public bool startAwake;

    private void Start()
    {
        if(startAwake)
        {
            start();

        }
    }
    public void start()
    {
        GetComponent<Animator>().SetTrigger(Transtiion);
    }
}
