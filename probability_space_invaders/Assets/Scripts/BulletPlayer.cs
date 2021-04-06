using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float force = 600f, destroyTime = 1f;
    Rigidbody2D rb;
    public GameObject ExplosionPrefab;
    private PlayerController playerController;

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

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Alien")){
            Destroy(collision.gameObject);
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            playerController.Score += 50; // FAIRE ALÉATOIRE
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("BulletAlien")){
            Destroy(collision.gameObject);
            playerController.Score += 10; // Aléatoire
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;


        }
    }
}
