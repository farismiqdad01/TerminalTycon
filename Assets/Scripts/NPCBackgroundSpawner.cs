using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCBackgroundSpawner : MonoBehaviour
{
    public GameObject[] npcprefabs;
    public GameObject[] vehiclePrefabs;
    public Transform NpcStartPos;
    public Transform vehicleStartPos;
    public bool reverse;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnNpc());
        StartCoroutine(spawnVehicle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnNpc()
    {
        yield return new WaitForSeconds(Random.Range(1,5));
        int rand = Random.Range(0, 100);
        Vector3 pos = NpcStartPos.position;
        bool direct;
        if (rand < 50)
        {
            pos = new Vector3(NpcStartPos.position.x, NpcStartPos.position.y, 0);
            direct = false;
        }
        else
        {
            pos = new Vector3(-NpcStartPos.position.x, NpcStartPos.position.y, 0);
            direct = true;

        }


        GameObject n = Instantiate(npcprefabs[Random.Range(0, npcprefabs.Length)], pos, Quaternion.identity, NpcStartPos.transform);
        n.transform.localPosition = new Vector3(n.transform.localPosition.x, n.transform.localPosition.y, 0);
        n.GetComponent<NpcBg>().right = direct;
        Destroy(n, 14);

        StartCoroutine(spawnNpc());
    }

    IEnumerator spawnVehicle()
    {
        yield return new WaitForSeconds(Random.Range(5, 8));
        Vector3 pos = vehicleStartPos.position;


        GameObject n = Instantiate(vehiclePrefabs[Random.Range(0, vehiclePrefabs.Length)], pos, Quaternion.identity, vehicleStartPos.transform);
        n.transform.localPosition = new Vector3(n.transform.localPosition.x, n.transform.localPosition.y, 0);
            n.GetComponent<BusBg>().reverse = reverse;
        Destroy(n, 14);

        StartCoroutine(spawnVehicle());
    }

}
