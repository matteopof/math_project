using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] alienType;
    public float spaceColumns = 2f, spaceRow = 2f;
    public int TotalAlienInLine = 6;
    public bool canMove = true;
    public bool walkRight = true;
    public float waveStepRight = 1f, waveStepDown = 1f, waveSpeed = 0.8f;

    //SoundWave à ajouter peut-être

    //Gestion nombre d'aliens
    public int totalAliensInWave;
    public int remainingAliens;

    //Redemarrage vague
    Vector2 positionInitialWave;
    PlayerController playerController;

    
    private void Awake(){
        // Générateur de vague d'alien
        for(int i = 0; i<alienType.Length; i++){
            float posY = transform.position.y - (spaceRow * i);
            for( int j = 0; j<TotalAlienInLine; j++){
                Vector2 pos = new Vector2(transform.position.x + spaceColumns * j, posY);
                GameObject Go = Instantiate(alienType[i].gameObject, pos, Quaternion.identity);
                Go.transform.SetParent(this.transform);
                Go.name = "Alien" + (j+1) + "-row:" + (i+1);
            }
        }
        //Assignation du nombre d'aliens
        totalAliensInWave = transform.childCount;
        remainingAliens = totalAliensInWave;
        
        //Position initiale
        positionInitialWave = transform.position;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
   
    }
    private void Start(){
        StartCoroutine(moveWave());
    }
    IEnumerator moveWave(){
        while(canMove){
            isWaveEmpty();
            Vector2 direction = walkRight ? Vector2.right : Vector2.left;
            transform.Translate(direction * waveStepRight);
            BroadcastMessage("animateAlien", SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(waveSpeed);
        }
    }
    public void waveTouchBumper(){
        walkRight = !walkRight;
        transform.Translate(Vector2.down * waveStepDown);
    }

    void isWaveEmpty(){
        if(remainingAliens==0){
            print("Won this level");
            GameObject.Find("SpawnPointUfo").GetComponent<SpawnUfo>().ufoStopSpawn();
            StopAllCoroutines();
        }
    }

    public void stopWave(){
        StopAllCoroutines();
    } 

    public void restartWave(float delay){
        StartCoroutine(restart(delay));
    }

    IEnumerator restart(float delay){
        yield return new WaitForSeconds(delay);
        transform.position = positionInitialWave;
        StartCoroutine(moveWave());
        playerController.initPlayer();
    }
}
