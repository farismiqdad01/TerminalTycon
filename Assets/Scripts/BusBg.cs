using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusBg : MonoBehaviour
{
    public Animator anim;
    public float speed;
    public bool reverse;

    // Update is called once per frame
    void Update()
    {
        if(reverse)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        }

        //anim.SetBool("Move", true);
    }
}
