using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject ufo;
    //Aleatoire
    public float firstSpawn = 1f, nextSpawn = 3f;

    // Start is called before the first frame update
    void Start()
    {
        firstSpawn = Random.Range(3f, 6f);
        nextSpawn = Random.Range(3f, 6f);
        InvokeRepeating("spawnUfoPrefab", firstSpawn, nextSpawn);
    }

    void spawnUfoPrefab(){
        if(!GameObject.Find("UFO")){
            GameObject go;
            go = Instantiate(ufo, transform.position, Quaternion.identity);
            go.name = "UFO";
        }
    }

    public void ufoStopSpawn(){
        CancelInvoke();
    }
}
