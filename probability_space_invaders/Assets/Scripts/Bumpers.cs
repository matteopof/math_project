using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpers : MonoBehaviour
{
    bool detect = true;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Alien") && detect){
            detect = false;
            collision.GetComponentInParent<Wave>().waveTouchBumper();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait(){
        yield return new WaitForSeconds(0.2f);
        detect = true;
    }
}
