using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    public Animator[] animNPC;
    public int bonusBayar;
    public int makanKapasitas;
    public float KecepatanMaju;
    public Image Ticket;
    public GameObject[] Masker;
    [HideInInspector] public float maxTimeAngry;
    [HideInInspector]public Transform targetWaitingPlace;
    [HideInInspector]public int bNumber;
    [HideInInspector]public float maxPos;
    [SerializeField] private Text bNumber_Text;
    [SerializeField] private Text kapasitas_Text;
    [HideInInspector] public bool ngantri;
    [HideInInspector]public bool gakPakeMasker;

    [Space(10)]
    [SerializeField] private Sprite kotak;
    [SerializeField] private Sprite segitiga;
    [SerializeField] private Sprite bulat;



    float timeAngryCounter;
    GameManager Gm;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < animNPC.Length; i++)
        {
            animNPC[i].SetBool("isWalking", true);
        }
        int rand = Random.Range(1, 80);
        if(rand < 20)
        {
            //ke1
            bNumber = 1;
            Ticket.sprite = kotak;
        }if(rand >= 20 && rand < 50)
        {
            //ke2
            bNumber = 2;
            Ticket.sprite = segitiga;

        }
        if (rand >= 50 && rand < 80)
        {
            //ke3
            bNumber = 3;
            Ticket.sprite = bulat;

        }

        if (bNumber == 0)
        {
            bNumber = 1;
            Ticket.sprite = kotak;

            
        }

        int maskRand = Random.Range(0, 100);
        if(maskRand >= 80)
        {
            //gk pake masker
            for (int i = 0; i < Masker.Length; i++)
            {
                GetComponent<NpcSkinChanger>().hilangkanMasker();

            }
            gakPakeMasker = true;
        }

        maxTimeAngry = Random.Range(10, 15);
        
        Gm = GameObject.FindObjectOfType<GameManager>();



        //Random Kapasitas

        int hari = PlayerPrefs.GetInt("Hari", 1);
        if(animNPC.Length <= hari)
        {
            makanKapasitas = Random.Range(animNPC.Length, hari);
        }else
        {
            makanKapasitas = animNPC.Length;
        }
        kapasitas_Text.text = "X" + makanKapasitas;
    }

    // Update is called once per frame
    void Update()
    {
       

        if(targetWaitingPlace != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaitingPlace.position, KecepatanMaju * 7 * Time.deltaTime);
            
            

            //    //if(makanKapasitas + Gm.busNgantri[0].pengunjung < Gm.busNgantri[0].kapasitas && !Gm.busNgantri[0].lagiGerak)
            //    //{
            //    //    targetWaitingPlace = Gm.pintumasuk;
            //    //    if(Vector2.Distance(transform.position, targetWaitingPlace.position) < 1)
            //    //    {
            //    //        Gm.busNgantri[0].pengunjung += makanKapasitas;
            //    //        Destroy(gameObject);
            //    //        Debug.Log(makanKapasitas + Gm.busNgantri[0].pengunjung);
            //    //    }
            //    //}
        }
        else
        {
            
            if (transform.localPosition.x > maxPos + 100)
            {
                transform.Translate(Vector3.left * KecepatanMaju * Time.deltaTime);
                for (int i = 0; i < animNPC.Length; i++)
                {
                    animNPC[i].SetBool("isWalking", true);
                }
                ngantri = false;
            }
            else
            {
                for (int i = 0; i < animNPC.Length; i++)
                {
                    animNPC[i].SetBool("isWalking", false);
                }
                ngantri = true;
            }
            if (bNumber == 0)
            {
                bNumber = Random.Range(1, 4);
            }
        }
    }
}
