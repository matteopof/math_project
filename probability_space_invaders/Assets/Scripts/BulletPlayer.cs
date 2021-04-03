using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float force = 600f, destroyTime = 1f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Start()
    {
        rb.AddForce(Vector2.up * force);
        Destroy(gameObject, destroyTime);

    }

    private void OnDestroy(){
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canShoot = true;
    }
}
