using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipUfo : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    private PlayerController playerController;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Start()
    {
        //liste des probabilités
        double[] proba = new double[8];

        // proba[0] = p(speed = 3f)
        // proba[7] = p(speed = 10f)

        //calcul du paramètre p
        double param = 0.8; // changer en fonction du score du joueur
        int score = playerController.Score;
        if (score <= 2500)
        {
            param = 0.5;
        }
        if (score > 2500 && score <= 6000)
        {
            param = 0.3;
        }
        if (score > 6000)
        {
            param = 0.1;
        }
        // calcul des probabilités 
        for (int i = 0; i < 8; i++)
        {
            proba[i] = param * System.Math.Pow((1 - param), i + 3 - 1); // p(x=k) = p(1-p)^(k-1)
        }

        double randomNumber = (double)UnityEngine.Random.Range(0f, 1f);
        print("randomSpeed = " + randomNumber);

        if (randomNumber >= 0 && randomNumber < proba[0])
        {
            print("speed=11f");
            speed = 11f;
        }
        if (randomNumber >= proba[0] && randomNumber < proba[0] + proba[1])
        {
            print("speed=10f");
            speed = 10f;
        }
        if (randomNumber >= proba[0] + proba[1] && randomNumber < proba[0] + proba[1] + proba[2])
        {
            print("speed=9f");
            speed = 9f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] && randomNumber < proba[0] + proba[1] + proba[2] + proba[3])
        {
            print("speed=8f");
            speed = 8f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] + proba[3] && randomNumber < proba[0] + proba[1] + proba[2] + proba[3] + proba[4])
        {
            print("speed=7f");
            speed = 7f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] + proba[3] + proba[4] && randomNumber < proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5])
        {
            print("speed=6f");
            speed = 6f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] && randomNumber < proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6])
        {
            print("speed=5f");
            speed = 5f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6] && randomNumber < proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6] + proba[7])
        {
            print("speed=4f");
            speed = 4f;
        }
        if (randomNumber >= proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6] + proba[7] && randomNumber <= 1)
        {
            double sum = proba[0] + proba[1] + proba[2] + proba[3] + proba[4] + proba[5] + proba[6] + proba[7];
            print("sum = " + sum);
            print("speed=3f");
            speed = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("Destroy")){
            Destroy(gameObject);
        }
    }
}
