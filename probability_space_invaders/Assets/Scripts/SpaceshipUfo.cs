using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipUfo : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(3f, 6f);
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
