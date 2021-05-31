
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUfo : MonoBehaviour
{
    public GameObject ufo;
    //Aleatoire
    public float firstSpawn = 1f, nextSpawn = 25f;
    private PlayerController playerController;
    float difficulty = Globals.slidervalfloat;
       
    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private int fact(int k)
    {
        if (k < 0)
        {
            return -1;
        }
        else if (k == 1 || k == 0)
        {
            return 1;
        }
        else
        {
            return k * fact(k - 1);
        }
    }

    private double[] poissonLaw(double lambda)
    {
        double[] proba = new double[12];
        for (int i = 0; i < 12; i++)
        {
            proba[i] = (System.Math.Pow(lambda, i) / fact(i)) * System.Math.Exp(-1 * lambda); 
        }
        return proba;
    }

    private float randomNextSpawn()
    {
        int score = playerController.Score;
        double lambda = 1;
        if (score <= 2500)
        {
            print("diff" + difficulty);
            lambda = difficulty * 10f - 4f;
            if (lambda < 1)
            {
                lambda = 1;
            }
            
        }
        if (score > 2500 && score <= 6000)
        {
            lambda = difficulty * 10f - 1f;
            if (lambda < 1)
            {
                lambda = 4;
            }
        }
        if (score > 6000)
        {
            lambda = difficulty * 10f + 2f;
            if (lambda < 1)
            {
                lambda = 7;
            }
        }
        //print("lambda = " + lambda);

        //liste des probabilités
        double[] proba = poissonLaw(lambda);

        double randomNumber = (double)UnityEngine.Random.Range(0f, 1f);

        double min = 0;
        double max = proba[0];
        //print("proba[0] = " + proba[0]);

        for (int i = 0; i <= 10; i++)
        {
            if (randomNumber >= min && randomNumber <= max)
            {
                nextSpawn = 20f + (float)i;
            }
            min += proba[i];
            max += proba[i + 1];
        }
        //print("proba[11] = " + proba[11]);
        if (randomNumber >= min && randomNumber <= max)
        {
            nextSpawn = 31f;
        }
        if (randomNumber >= max && randomNumber <= 1)
        {
            nextSpawn = 32f;
        }

        print("nextSpawn = " + nextSpawn);
        return nextSpawn;
    }


    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = randomNextSpawn();
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

    // Update is called once per frame 
    void Update()
    {   
        nextSpawn -= Time.deltaTime;
        //print("nextSpawn = " + nextSpawn);
        if (nextSpawn <= 0)
        {
            spawnUfoPrefab();
            nextSpawn = randomNextSpawn();
        }
    }
}