using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject ufo;
    //Aleatoire
    public float firstSpawn = 1f, nextSpawn = 25f;
    private PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //liste des probabilités
        double[] proba = new double[11];
        // proba[0] = p(nextSpawn = 20f)
        // proba[10] = p(nextSpawn = 30f)

        //calcul du paramètre p
        int score = playerController.Score;
        double lambda = 1;
        if (score <= 2500)
        {
            lambda = 1;
        }
        if (score > 2500 && score <= 6000)
        {
            lambda = 4;
        }
        else
        {
            lambda = 10;
        }

        // calcul des probabilités 
        for (int i = 0; i < 8; i++)
        {
            double fact = i;

            for (double j = fact - 1; j >= 1; j--)
            {
                fact = fact * j;
                print(fact);
            }

            proba[i] = (System.Math.Pow(lambda, i) / fact) * System.Math.Exp(-1 * lambda); //formule poisson
        }
        // proba[7] = 1 - (proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6] + proba[7] + proba[8]);

        // random 
        double randomNumber = (double)UnityEngine.Random.Range(0f, 1f);
        //randomNumber += 20;
        //randomNumber *= 30;

        if (randomNumber >= 0 && randomNumber < proba[0])
        {
            print("nextSpawn = 20");
            nextSpawn = 20f;
        }
        if (randomNumber >= proba[0] && randomNumber < proba[1])
        {
            print("nextSpawn = 21");
            nextSpawn = 21f;
        }
        if (randomNumber >= proba[1] && randomNumber < proba[2])
        {
            print("nextSpawn = 22");
            nextSpawn = 22f;
        }
        if (randomNumber >= proba[2] && randomNumber < proba[3])
        {
            print("nextSpawn = 23");
            nextSpawn = 23f;
        }
        if (randomNumber >= proba[3] && randomNumber < proba[4])
        {
            print("nextSpawn = 24");
            nextSpawn = 24f;
        }
        if (randomNumber >= proba[4] && randomNumber < proba[5])
        {
            print("nextSpawn = 25");
            nextSpawn = 25f;
        }
        if (randomNumber >= proba[5] && randomNumber < proba[6])
        {
            print("nextSpawn = 26");
            nextSpawn = 26f;
        }
        if (randomNumber >= proba[6] && randomNumber < proba[7])
        {
            print("nextSpawn = 27");
            nextSpawn = 27f;
        }
        if (randomNumber >= proba[7] && randomNumber < proba[8])
        {
            print("nextSpawn = 28");
            nextSpawn = 28f;
        }
        if (randomNumber >= proba[8] && randomNumber < proba[9])
        {
            print("nextSpawn = 29");
            nextSpawn = 29f;
        }
        if (randomNumber >= proba[9] && randomNumber < proba[10])
        {
            print("nextSpawn = 30");
            nextSpawn = 30f;
        }
        

        firstSpawn = Random.Range(15f, 20f);
        //nextSpawn = Random.Range(20f, 30f); // loi de poisson
        InvokeRepeating("spawnUfoPrefab", firstSpawn, nextSpawn);
    }

    void spawnUfoPrefab()
    {
        if (!GameObject.Find("UFO"))
        {
            GameObject go;
            go = Instantiate(ufo, transform.position, Quaternion.identity);
            go.name = "UFO";
        }
    }

    public void ufoStopSpawn()
    {
        CancelInvoke();
    }

}
