using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bus : MonoBehaviour
{
    public int idBus;
    public GameObject GFX;
    [Header("Data")]
    public float speed;
    public int kapasitas;
    public double uangdidalam;
    public float spawnTime;
    [Space(10)]
    public int hargaPerPenumpang;
    public int[] hargaUpgrade;
    [Range(1,50)]public int maxLevel;

    [Header("UI")]
    public Text pengunjung_Text;
    public GameObject idleAudio;
    public GameObject moveAudio;
    public GameObject moveAwayAudio;


    private float Startspeed = 3;
    [HideInInspector]public float maxPos = -2.71f;
    [HideInInspector]public int pengunjung;
    [HideInInspector]public int levels;
    [HideInInspector]public bool udhJalan;
    [HideInInspector]public bool lagiGerak;
    GameManager Gm;
    // Start is called before the first frame update
    void Start()
    {
        Gm = GameObject.FindObjectOfType<GameManager>();

        levels = PlayerPrefs.GetInt("Bus" + idBus, 1);
        if(levels > 1)
        {
            kapasitas += (levels - 1) * 2;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x > maxPos + 600 && !udhJalan)
        {
            transform.Translate(Vector3.left * Startspeed * Time.deltaTime);
            GFX.GetComponent<Animator>().SetBool("Move", true);
            lagiGerak = true;

            idleAudio.SetActive(false);
            moveAudio.SetActive(true);
            moveAwayAudio.SetActive(false);
        }else
        {
            if(!udhJalan)
            {
                GFX.GetComponent<Animator>().SetBool("Move", false);
                lagiGerak = false;

                idleAudio.SetActive(true);
                moveAudio.SetActive(false);
                moveAwayAudio.SetActive(false);
            }
            
        }
        pengunjung_Text.text = "Kapasitas : " + pengunjung.ToString() + " / " + kapasitas;

        if (pengunjung >= kapasitas || udhJalan)
        {
            idleAudio.SetActive(false);
            moveAudio.SetActive(false);
            moveAwayAudio.SetActive(true);

            transform.Translate(Vector3.left * speed * Time.deltaTime);
            GFX.GetComponent<Animator>().SetBool("Move", true);

            Gm.busNgantri.Remove(this);
            maxPos = -30;
            udhJalan = true;

        }

        if(transform.localPosition.x <= -2000f)
        {
            Gm.uang += uangdidalam;
            spawnfront s = Gm.um.coinsPrefabs.GetComponent<spawnfront>();
            GameObject o = Instantiate(Gm.um.coinsPrefabs, Vector3.zero, Quaternion.identity, Gm.um.coinsSpawnerPoint);
            o.transform.localPosition = new Vector2(Random.Range(-s.sizeSpawnPos.x, s.sizeSpawnPos.x), Random.Range(-s.sizeSpawnPos.y, s.sizeSpawnPos.x));
            o.GetComponent<spawnfront>().value = "+" + uangdidalam.ToString();
            Destroy(o, 1);
            Destroy(gameObject);
            Debug.Log("a");
        }
        
    }
}
