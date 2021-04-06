using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public GameObject[] slots;
    public int remainingSlots;

    private void Awake(){
        slots = GameObject.FindGameObjectsWithTag("Slot");
        remainingSlots = slots.Length;
    }

    public void loseSlot(){
        remainingSlots -= 1;

        switch(remainingSlots){
            case 2 : slots[0].SetActive(false);
            break;

            case 1 : slots[1].SetActive(false);
            break;

            case 0 : slots[2].SetActive(false);
            gameOver();
            break;
        }
    }

    void gameOver(){
        print("Game Over");
    }
}
