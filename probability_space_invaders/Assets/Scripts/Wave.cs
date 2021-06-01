using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] alienType;
    public float spaceColumns = 2f, spaceRow = 2f;
    public int totalAliensInLine = 6;
    public int totalColums = 4;
    public bool canMove = true;
    public bool walkRight = true;
    public float waveStepRight = 1f, waveStepDown = 1f, waveSpeed = 0.6f;

    //SoundWave à ajouter peut-être

    //Gestion nombre d'aliens
    public int totalAliensInWave;
    public int remainingAliens;
    public int numberOfTypes = 8;

    //Redemarrage vague
    Vector2 positionInitialWave;
    PlayerController playerController;

    private int uniformLaw(){
        float sum = 0;
        float[] p = new float[numberOfTypes];
        //print(p.Length);
        for(int i = 0; i<p.Length; i++){
            p[i] = (float)1/numberOfTypes;
            //print("p"+i +"="+ p[i]);
            sum += p[i];
        }
        //print("sum" + sum);

        // Random between 0 and 1
        double randomNumber = (double)UnityEngine.Random.Range(0f, 1f);

        //print("Random number " + randomNumber);

        int indexNumber = 0;
        double min = 0;
        double max = p[0];

        for (int i = 0; i<=numberOfTypes-2; i++)
        {
            if (randomNumber >= min && randomNumber <= max)
            {
                indexNumber = i;
            }
            min += p[i];
            max += p[i + 1];
        }
        if (randomNumber >= min && randomNumber <= max)
        {
            indexNumber = numberOfTypes-1;
        }
        //print("L'indexe renvoyé est : " + indexNumber);
        return indexNumber;
    }
    
    
    private void Awake(){
        // Générateur de vague d'alien
        for(int i = 0; i<totalColums; i++){
            float posY = transform.position.y - (spaceRow * i);
            for( int j = 0; j<totalAliensInLine; j++){
                Vector2 pos = new Vector2(transform.position.x + spaceColumns * j, posY);
                GameObject Go = Instantiate(alienType[uniformLaw()].gameObject, pos, Quaternion.identity);
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
            StopAllCoroutines();
            // GameObject.Find("SpawnPointUfo").GetComponent<SpawnUfo>().ufoStopSpawn();
            generateNewWave();

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

    void generateNewWave(){

        //Ajout d'un alien par ligne
        if(totalAliensInLine<16) totalAliensInLine += 1;

        // Générateur de vague d'alien
        for(int i = 0; i<totalColums; i++){
            float posY = transform.position.y - (spaceRow * i);
            for( int j = 0; j<totalAliensInLine; j++){
                Vector2 pos = new Vector2(transform.position.x + spaceColumns * j, posY);
                GameObject Go = Instantiate(alienType[uniformLaw()].gameObject, pos, Quaternion.identity);
                Go.transform.SetParent(this.transform);
                Go.name = "Alien" + (j+1) + "-row:" + (i+1);
            }
        }
        //Assignation du nombre d'aliens
        totalAliensInWave = transform.childCount;
        remainingAliens = totalAliensInWave;

        // JOUER AVEC L'ALÉATOIRE ICI

        //Position initiale vague
        transform.position = positionInitialWave;

        //Accélération vague
        if(waveSpeed> 0.2f) waveSpeed -= 0.2f;

        //Accélération cadence de tirs aliens
        if(playerController.alienShootRate > 1f) playerController.alienShootRate -= 0.1f;
        StartCoroutine(moveWave());
    }
}
