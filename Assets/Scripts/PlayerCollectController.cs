using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollectController : MonoBehaviour
{
    private int fruits = 0;
    [SerializeField] private Text fruitsText;
    [SerializeField] private AudioSource fruitPickingSound;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Apple")){
            Destroy(collider.gameObject);
            AddFruitToCounter(5);
        }else if(collider.gameObject.CompareTag("Strawberry")){
            Destroy(collider.gameObject);
            AddFruitToCounter(3);
        }else if(collider.gameObject.CompareTag("Orange")){
            Destroy(collider.gameObject);
            AddFruitToCounter(2);
        }else if(collider.gameObject.CompareTag("Melon")){
            Destroy(collider.gameObject);
            AddFruitToCounter(15);
        }else if(collider.gameObject.CompareTag("Pinapple")){
            Destroy(collider.gameObject);
            AddFruitToCounter(7);
        }
    }

    private void AddFruitToCounter(int amount){
            fruits += amount;
            fruitsText.text = "Fruits: " + fruits;
            fruitPickingSound.Play();
    }
}
