using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 positionPlayer;
    float speed = 5f;
    float limitX = 7.6f;
    public GameObject bulletPrefab;
    Transform ejectPosition;
    public bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        positionPlayer = transform.position;
        ejectPosition = transform.Find("Eject");
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        playerShoot();
    }

    void movePlayer(){
        positionPlayer.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        positionPlayer.x = Mathf.Clamp(positionPlayer.x, -limitX, limitX);
        transform.position = positionPlayer;
    }

    void playerShoot(){
        if(Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            canShoot=false;
            Instantiate(bulletPrefab, ejectPosition.position, Quaternion.identity);
        }
    }
}
