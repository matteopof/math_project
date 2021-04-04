using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAlien : MonoBehaviour
{
   SpriteRenderer sr;
   [SerializeField] float delay = 0.5f; 
   AudioSource audioSource;

   private void Awake(){
       sr = GetComponent<SpriteRenderer>();
       audioSource = GetComponent<AudioSource>();
   }

   private void Start(){
       StartCoroutine(destroyExplosion());
   }

   IEnumerator destroyExplosion(){
       audioSource.Play();
       yield return new WaitForSeconds(delay);
       GameObject.Find("Wave").GetComponent<Wave>().remainingAliens -= 1;
       Destroy(this.gameObject, delay);
   }
}
