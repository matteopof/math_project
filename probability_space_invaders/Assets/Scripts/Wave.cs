using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] alienType;
    public float spaceColumns = 2f, spaceRow = 2f;
    public int TotalAlienInLine = 6;

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
    }
}
