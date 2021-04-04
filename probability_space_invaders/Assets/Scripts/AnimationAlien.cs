using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAlien : MonoBehaviour
{
    public Sprite sprite2;
    Sprite sprite1;
    private void Awake(){
        sprite1 = GetComponent<SpriteRenderer>().sprite;
    }
    void animateAlien(){
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Sprite nextSprite = sr.sprite == sprite1 ? sprite2 : sprite1;
        sr.sprite = nextSprite;
    }
}
