using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float force = 600f, destroyTime = 1f;
    Rigidbody2D rb;
    public GameObject ExplosionPrefab;
    private PlayerController playerController;
    float difficulty = Globals.slidervalfloat;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Start()
    {
        rb.AddForce(Vector2.up * force);
        Destroy(gameObject, destroyTime);

    }

    private void OnDestroy(){
        playerController.canShoot = true;
    }

    private int fact(int k)
    {
        if (k == 0)
        {
            return 1;
        }
        for (int j = k - 1; j >= 1; j--)
        {
            k = k * j;
        }
        return k;
    }

    private float[] binomialLaw(float p)
    {
        float[] proba = new float[6];
        for (int i = 0; i<=5; i++)
        {
            float coeffBinom = 120f / (float)(fact(i) * fact(5 - i));
            proba[i] = coeffBinom * (float)System.Math.Pow(p, i) * (float)System.Math.Pow(1 - p, 5 - i);
        }
        return proba;
    }

    private int RandomScore()
    {
        int playerScore = playerController.Score;
        float bernoulliProba = (difficulty * 0.4f) + 0.3f; //0.5f is a good value
        if (playerScore <= 2500)
        {
            bernoulliProba = bernoulliProba - 0.3f;
            if(bernoulliProba < 0)
            {
                bernoulliProba = 0f;
            }
        }
        if (playerScore > 2500 && playerScore <= 6000)
        {
            bernoulliProba = bernoulliProba - 0.1f;
            if (bernoulliProba < 0)
            {
                bernoulliProba = bernoulliProba + 0f;
            }
        }
        if (playerScore > 6000)
        {
            bernoulliProba = bernoulliProba + 0.1f;
            if (bernoulliProba > 1)
            {
                bernoulliProba = 1f;
            }
        }

        float rdmBernoulli = UnityEngine.Random.Range(0f, 1f);
        if (rdmBernoulli > bernoulliProba)
        {
            //print("player gets no bonus");
            int score = 50;
            return score;
        }
        else
        {
            float binomProba = (difficulty * 0.4f) + 0.3f; //0.5f is a good value
            if (playerScore <= 2500)
            {
                binomProba = binomProba + 0.2f;
            }
            if (playerScore > 2500 && playerScore <= 6000)
            {
                binomProba = binomProba + 0f;
            }
            if (playerScore > 6000)
            {
                binomProba = binomProba - 0.2f;
            }
            float rdmBinom= UnityEngine.Random.Range(0f, 1f);
            float[] proba = binomialLaw(binomProba);

            int bonus = 0;
            double min = 0;
            double max = proba[0];

            for (int i = 0; i <= 4; i++)
            {
                if (rdmBinom >= min && rdmBinom <= max)
                {
                    bonus = i;
                }
                min += proba[i];
                max += proba[i + 1];
            }
            if (rdmBinom >= min && rdmBinom <= max)
            {
                bonus = 5;
            }
            if (rdmBinom >= max && rdmBinom <= 1)
            {
                bonus = 6;
            }
            int score = 50 + (bonus * 20 - 50);
            //print("SCORE = " + score + "(bonus = " + bonus +")");
            return score;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Alien")){
            Destroy(collision.gameObject);
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            playerController.Score += RandomScore(); // FAIRE ALÉATOIRE
            Destroy(this.gameObject);
        }

        if(collision.gameObject.CompareTag("Ufo")){
            Destroy(collision.gameObject);
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            GameObject.Find("Wave").GetComponent<Wave>().remainingAliens += 1;
            playerController.Score += 200; 
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("BulletAlien")){
            Destroy(collision.gameObject);
            playerController.Score += 10; 
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
