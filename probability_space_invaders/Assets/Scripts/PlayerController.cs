 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 positionPlayer;
    float speed = 5f;
    float limitX = 7.6f;
    public GameObject bulletPrefab;
    Transform ejectPosition;
    public bool canShoot = true;
    Text txtScore;
    Wave waveScript;
    bool detect = true;
    public GameObject alienBullet;
    bool alienCanShoot = true;
    int layerDefault;
    public float alienShootRate = 1f;
    float difficulty = Globals.slidervalfloat;

    private int score = 0; 
    public int Score{
        get{
            return score;
        }
        set{
            score = value;
            txtScore.text = "Score : " + score;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        positionPlayer = transform.position;
        ejectPosition = transform.Find("Eject");
        txtScore = GameObject.Find("TxtScore").GetComponent<Text>();
        waveScript = GameObject.Find("Wave").GetComponent<Wave>();
        layerDefault = LayerMask.GetMask("Default");
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        playerShoot();
        alienShoot();
        //print("slider value " + difficulty);
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

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Alien") & detect || collision.CompareTag("BulletAlien")){
            detect =false;
            StartCoroutine(alienKillPlayer());
            print("player dead");
        }
    }

    IEnumerator alienKillPlayer(){
        waveScript.stopWave();
        playerExplosion();
        GameObject.Find("TxtLives").GetComponent<Lives>().loseSlot();
        yield return new WaitForSeconds(0.2f);
        detect = true;
        waveScript.restartWave(1f);
    }

    void playerExplosion(){
        GetComponent<Animator>().SetTrigger("explosion");
        GetComponent<AudioSource>().Play();
        canShoot = false;
    }

    public void initPlayer(){
        GetComponent<Animator>().SetTrigger("normal");
        canShoot = true;
    }

    void alienShoot(){
        Debug.DrawRay(transform.position, Vector2.up *5);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, Mathf.Infinity, layerDefault);
        if(hit.collider!=null){
            if(hit.collider.CompareTag("Alien") && alienCanShoot){
                StartCoroutine(pause());
                GameObject go = Instantiate(alienBullet, hit.point, Quaternion.identity);
                Destroy(go, 10f);
            }
        }
    }

    IEnumerator pause(){
        alienCanShoot = false;
        yield return new WaitForSeconds(alienShootRate);
        alienCanShoot = true;
    }

    
}
