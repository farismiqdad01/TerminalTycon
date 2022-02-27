using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcBg : MonoBehaviour
{
    public Animator anim;
    public float speed;
   [HideInInspector] public bool right;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(right)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            anim.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            anim.transform.localScale = new Vector3(-1, 1, 1);


        }
        anim.SetBool("isWalking", true);
    }
}
