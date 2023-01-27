using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCollectController : MonoBehaviour
{
    private int apples = 0;
    [SerializeField] private Text fruitsText;
    [SerializeField] private AudioSource fruitPickingSound;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Apple")){
            Destroy(collider.gameObject);
            apples++;
            fruitsText.text = "Fruits: " + apples;
            fruitPickingSound.Play();
        }


    }
}
