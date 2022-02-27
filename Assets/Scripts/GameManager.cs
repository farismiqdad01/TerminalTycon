using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    public bool deletePref;
    public double uang;
    public int Popularity;
    public float KecepatanMaju;
    public float TolakRate;

    [Header("Bus System")]
    public float KecepatanBus;
    public List<GameObject> BusPrefabs = new List<GameObject>();
    public List<GameObject> BusPrefabsData = new List<GameObject>();
    public List<Bus> busNgantri = new List<Bus>();
    public Transform BUSspawnpoint, BUSmaxpoint;


    [Header("Npc System")]
    public GameObject[] npcPrefabs;
    public List<Npc> npcNgantri = new List<Npc>();
    public Transform waitingplace, pintumasuk, pintuKeluar;
    public Transform NPCspawnpoint, NPCmaxpoint;
    public Text hari_text, pengunjung_Text, uang_Text;


    float nextTimetoTolak;
    int hari;
    [HideInInspector]public List<float> busSpawnTime = new List<float>();
    [HideInInspector] public List<float> busSpawnCounter = new List<float>();

    [SerializeField] private GameObject tutorialMenu;
    [HideInInspector]public UiManager um;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("tutor", 0) == 0)
        {
            tutorialMenu.SetActive(true);
            PlayerPrefs.SetInt("tutor", 1);
        }
        um = GetComponent<UiManager>();

        updateBusSpawner();
        StartCoroutine(spawnNPC());
        hari = 1;
        PlayerPrefs.SetFloat("BgmVolume", 0.1f);


        //Saving data level Bis

        for (int i = 1; i < BusPrefabsData.Count; i++)
        {
            if(PlayerPrefs.HasKey("Bus" + i + "Beli"))
            {
                BusPrefabs.Add(BusPrefabsData[i]);
                updateBusSpawner();
                um.updateLevelsDisplay();
                um.disbleBeli(i);
            }
        }
         uang = PlayerPrefs.GetFloat("Uang", 0);
         Popularity = PlayerPrefs.GetInt("Popularity", 0);
         um.hari = PlayerPrefs.GetInt("Hari", 1);



        if (deletePref)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("Bus0", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
            NpcSystem();
            BusSystem();
        hari_text.text = "Hari Ke-" + hari.ToString();


       // UangSystem();
        UangFormat();

    }

    void UangFormat()
    {
        
        uang_Text.text = string.Format("{0:#,###0}", uang);
    }

    void UangSystem()
    {
        if (uang >= 1000 && uang < 1000000)
        {
            string u = uang.ToString();
            u = u.Remove(u.Length - 3);
            uang_Text.text = "Uang : " + u + "k";
        }
        else if (uang >= 1000000 && uang < 1000000000)
        {
            string u = uang.ToString();
            u = u.Remove(u.Length - 6);
            uang_Text.text = "Uang : " + u + "jt";
        }
        else if (uang >= 1000000000 && uang < 1000000000000)
        {
            string u = uang.ToString();
            u = u.Remove(u.Length - 9);
            uang_Text.text = "Uang : " + u + "m";
        }
        else if (uang >= 1000000000000 && uang <= 9999999999999)
        {
            string u = uang.ToString();
            u = u.Remove(u.Length - 12);
            uang_Text.text = "Uang : " + u + "b";
        }
        else if (uang >= 9999999999999)
        {
            uang_Text.text = "9999" + "b";
        }
        else
        {
            uang_Text.text = "Uang : " + uang.ToString();

        }
    }
    void BusSystem()
    {
        if (busNgantri.Count > 0)
        {
            busNgantri[0].maxPos = BUSmaxpoint.transform.localPosition.x;
            for (int i = 0; i < busNgantri.Count; i++)
            {
                if (i + 1 < busNgantri.Count)
                {
                    busNgantri[i + 1].maxPos = busNgantri[i].transform.localPosition.x;
                }
            }
        }
        if(busSpawnCounter.Count > 0)
        {
            for (int i = busSpawnCounter.Count - 1; i >= 0; i--)
            {
                if (busNgantri.Count < 3 && busSpawnCounter[i] >= busSpawnTime[i] - 1 && PlayerPrefs.GetInt("Bus" + i + "Activate", 1) == 1)
                {
                    busSpawnCounter[i] = 0;
                    spawnBus(i);
                }
            }

            for (int i = 0; i < busSpawnCounter.Count; i++)
            {
                if (busSpawnCounter[i] <= busSpawnTime[i] && PlayerPrefs.GetInt("Bus" + i + "Activate", 1) == 1)
                {
                    busSpawnCounter[i] += 1 * Time.deltaTime;

                }

                if (busSpawnCounter[i] >= busSpawnTime[i] && busNgantri.Count < 3 && PlayerPrefs.GetInt("Bus" + i + "Activate", 1) == 1)
                {
                    busSpawnCounter[i] = 0;

                    spawnBus(i);
                }



            }

           
        }

        if (busNgantri.Count > 0 &&  busNgantri[0].lagiGerak == false)
        {
            um.berangkatButton.GetComponent<Image>().sprite = um.berangkatNothingSprite;
        }else
        {
            um.berangkatButton.GetComponent<Image>().sprite = um.berangkatClickSprite;

        }

    }
    void NpcSystem()
    {
        if (npcNgantri.Count > 0)
        {
            npcNgantri[0].maxPos = NPCmaxpoint.transform.localPosition.x;
            for (int i = 0; i < npcNgantri.Count; i++)
            {
                if (i + 1 < npcNgantri.Count)
                {
                    npcNgantri[i + 1].maxPos = npcNgantri[i].transform.localPosition.x;
                }
            }
        }
    }

    void updateBusSpawner()
    {
        if (BusPrefabs.Count > 0)
        {
            //busSpawnTime.RemoveRange(0, busSpawnTime.Count);
            //busSpawnCounter.RemoveRange(0, busSpawnCounter.Count);



            //for (int i = 0; i < um.busBarItem.Count; i++)
            //{
            //    Destroy(um.busBarItem[i]);
            //}
            //um.busBarItem.RemoveRange(0, um.busBarItem.Count);

            //for (int i = 0; i < BusPrefabs.Count; i++)
            //{
                busSpawnTime.Add(BusPrefabs[BusPrefabs.Count - 1].GetComponent<Bus>().spawnTime);
                busSpawnCounter.Add(0);

                GameObject bar = Instantiate(um.busBarItemPrefabs, um.busBarGrid);
                bar.GetComponent<BarBusItem>().nameTitle = BusPrefabs[BusPrefabs.Count - 1].name;
                bar.GetComponent<BarBusItem>().id = BusPrefabs.Count - 1;
            bar.transform.SetAsFirstSibling();
                um.busBarItem.Add(bar);
            //}
        }
    }

    void spawnBus(int index)
    {
        GameObject bus = Instantiate(BusPrefabs[index], BUSspawnpoint.position, BUSspawnpoint.rotation, BUSspawnpoint.parent);
        busNgantri.Add(bus.GetComponent<Bus>());

    }



    IEnumerator spawnNPC()
    {
        yield return new WaitForSeconds(Random.Range(3, 8) - Popularity * 0.01f);
        if(npcNgantri.Count < 15 && PlayerPrefs.GetInt("tutorfinish", 0) == 1)
        {
            GameObject npc = Instantiate(npcPrefabs[Random.Range(0, npcPrefabs.Length)], NPCspawnpoint.position, NPCspawnpoint.rotation, NPCspawnpoint.parent);
            npcNgantri.Add(npc.GetComponent<Npc>());
        }
        
        StartCoroutine(spawnNPC());
    }

    public void clickB(int num)
    {
        if(busNgantri.Count > 0 && npcNgantri.Count > 0)
        {
            if (npcNgantri[0].ngantri && !busNgantri[0].lagiGerak)
            {
                if (npcNgantri[0].bNumber == num)
                {
                    if (npcNgantri[0].makanKapasitas + busNgantri[0].pengunjung > busNgantri[0].kapasitas)
                    {
                        //Gk Muat kebanyakan orang
                        //npcNgantri[0].targetWaitingPlace = waitingplace;
                        //npcNgantri.RemoveAt(0);
                        //Debug.Log("Harus Nunggu");
                        busNgantri[0].pengunjung_Text.GetComponent<Animator>().SetTrigger("Maximal");
                    }
                    else
                    {
                        if (npcNgantri[0].gakPakeMasker)
                        {
                            Debug.Log("DIA GK PAKE MASKER!!!");
                            Popularity--;

                            GameObject p = Instantiate(um.popularPrefabs, um.popularPrefabs.transform.position, Quaternion.identity, um.popularSpawnerPoint);
                            p.transform.localPosition = Vector3.zero;
                            p.GetComponent<spawnfront>().value = "-2";

                            Destroy(p, 2);
                        }
                        else
                        {
                            Popularity++;

                            GameObject p = Instantiate(um.popularPrefabs, um.popularPrefabs.transform.position, Quaternion.identity, um.popularSpawnerPoint);
                            p.transform.localPosition = Vector3.zero;
                            p.GetComponent<spawnfront>().value = "+1";

                            Destroy(p, 2);
                        }
                        //uang += npcNgantri[0].bonusBayar;
                        busNgantri[0].uangdidalam += busNgantri[0].hargaPerPenumpang * npcNgantri[0].makanKapasitas;
                        busNgantri[0].pengunjung += npcNgantri[0].makanKapasitas;
                        Destroy(npcNgantri[0].gameObject);
                        npcNgantri.RemoveAt(0);
                    }

                }
                else
                {
                    Debug.Log("Salah Tujuan!!!");
                    Popularity--;

                    GameObject p = Instantiate(um.popularPrefabs, um.popularPrefabs.transform.position, Quaternion.identity, um.popularSpawnerPoint);
                    p.transform.localPosition = Vector3.zero;
                    p.GetComponent<spawnfront>().value = "-1";

                    Destroy(p, 2);
                }

                PlayerPrefs.SetFloat("Uang", (float)uang);
                PlayerPrefs.SetInt("Popularity", Popularity);

            }
        }

        

        um.buttonClickObject.GetComponent<AudioSource>().clip = um.buttonClickClip[Random.Range(0, um.buttonClickClip.Length)];
        um.buttonClickObject.GetComponent<AudioSource>().Play();

    }

    public void Tolak()
    {
        if(Time.time >= nextTimetoTolak)
        {
            nextTimetoTolak = Time.time + 1f / TolakRate;

            if (npcNgantri.Count > 0)
            {
                Destroy(npcNgantri[0].gameObject, 5);
                npcNgantri[0].targetWaitingPlace = pintuKeluar;
                npcNgantri.RemoveAt(0);

                PlayerPrefs.SetInt("Popularity", Popularity);
            }
        }
        um.buttonClickObject.GetComponent<AudioSource>().clip = um.buttonClickClip[Random.Range(0, um.buttonClickClip.Length)];
        um.buttonClickObject.GetComponent<AudioSource>().Play();


    }

    public void berangkatsekarang()
    {
        if(busNgantri.Count > 0 && !busNgantri[0].lagiGerak)
        {
            busNgantri[0].maxPos = -30;
            busNgantri[0].udhJalan = true;
            busNgantri.RemoveAt(0);

            um.buttonClickObject.GetComponent<AudioSource>().clip = um.buttonClickClip[Random.Range(0, um.buttonClickClip.Length)];
            um.buttonClickObject.GetComponent<AudioSource>().Play();
        }
        

    }



    public void belibus(GameObject busObjectPrefabs)
    {
        if(uang >= busObjectPrefabs.GetComponent<Bus>().hargaUpgrade[0])
        {
            PlayerPrefs.SetInt("Bus" + busObjectPrefabs.GetComponent<Bus>().idBus.ToString(), 1);
            PlayerPrefs.SetInt("Bus" + busObjectPrefabs.GetComponent<Bus>().idBus.ToString() + "Beli", 1);
            BusPrefabs.Add(busObjectPrefabs);
            updateBusSpawner();
            um.updateLevelsDisplay();

            um.tokoBus[busObjectPrefabs.GetComponent<Bus>().idBus].beliButton.SetActive(false);
            um.tokoBus[busObjectPrefabs.GetComponent<Bus>().idBus].upgradeButton.SetActive(true);
            um.tokoBus[busObjectPrefabs.GetComponent<Bus>().idBus].level.gameObject.SetActive(true);

            int total = busObjectPrefabs.GetComponent<Bus>().hargaUpgrade[0];
            uang -= total;

            PlayerPrefs.SetFloat("Uang", (float)uang);

            spawnfront s = um.coinsPrefabsRed.GetComponent<spawnfront>();
            GameObject o = Instantiate(um.coinsPrefabsRed, Vector3.zero, Quaternion.identity, um.coinsSpawnerPoint);
            o.transform.localPosition = new Vector2(Random.Range(-s.sizeSpawnPos.x, s.sizeSpawnPos.x), Random.Range(-s.sizeSpawnPos.y, s.sizeSpawnPos.x));
            o.GetComponent<spawnfront>().value = "-" + total.ToString();
            Destroy(o, 1);

        }


    }

    public void upgradeBus(int idBus)
    {
        int har = BusPrefabsData[idBus].GetComponent<Bus>().hargaUpgrade[PlayerPrefs.GetInt("Bus" + idBus)];
        Debug.Log(har);
        if (uang >= har)
        {
            int curLev = PlayerPrefs.GetInt("Bus" + idBus, 1);
            PlayerPrefs.SetInt("Bus" + idBus, curLev + 1);
            um.updateLevelsDisplay();


            uang -= har;
            PlayerPrefs.SetFloat("Uang", (float)uang);

            spawnfront s = um.coinsPrefabsRed.GetComponent<spawnfront>();
            GameObject o = Instantiate(um.coinsPrefabsRed, Vector3.zero, Quaternion.identity, um.coinsSpawnerPoint);
            o.transform.localPosition = new Vector2(Random.Range(-s.sizeSpawnPos.x, s.sizeSpawnPos.x), Random.Range(-s.sizeSpawnPos.y, s.sizeSpawnPos.x));
            o.GetComponent<spawnfront>().value = "-" + har.ToString();
            Destroy(o, 1);
        }

    }


}
