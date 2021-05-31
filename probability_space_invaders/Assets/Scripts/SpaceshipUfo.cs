using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipUfo : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    private PlayerController playerController;
    float difficulty = Globals.slidervalfloat;

    void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private double[] geometricLaw(double param)
    {
        //liste des probabilités
        double[] proba = new double[8];
        // proba[0] = p(x=1) = p(speed = 3f)
        // proba[8] = p(x=8) = p(speed = 10f)

        // calcul des probabilités 
        for (int i = 0; i < 8; i++)
        {
            proba[i] = param * System.Math.Pow((1 - param), i); // p(x=k) = p(1-p)^(k-1)
        }

        return proba;
    }


    private float randomSpeed()
    {
        double param = (double)difficulty * 0.6;
        int score = playerController.Score;
        if (score <= 2500)
        {
            param = param + 0.2;
            if (param >= 1)
            {
                param = 0.9;
            }
        }
        if (score > 2500 && score <= 6000)
        {
            param = param + 0;
        }
        if (score > 6000)
        {
            param = param - 0.2;
            if(param <= 0)
            {
                param = 0.1;
            }
        }

        double randomNumber = (double)UnityEngine.Random.Range(0f, 1f);
        //print("randomSpeed = " + randomNumber);
        double[] proba = geometricLaw(param);

        double min = 0;
        double max = proba[0];

        for (int i = 0; i <= 6; i++)
        {
            if(randomNumber >= min && randomNumber <= max)
            {
                speed = i + 3f;
            }
            min += proba[i];
            max += proba[i + 1];
        }
        if (randomNumber >= min && randomNumber <= max)
        {
            speed = 10f;
        }
        if (randomNumber >= max && randomNumber <= 1)
        {
            speed = 11f;
        }

        //print("speed =" + speed);
        return speed;
    }
    void Start()
    {
        speed = randomSpeed();
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
