using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevelController : MonoBehaviour
{
    [SerializeField] private AudioSource levelCompletionSound;
    // Start is called before the first frame update
    void Start()
    {
        levelCompletionSound = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.name == "Player"){
            levelCompletionSound.Play();
            CompleteLevel();
        }
    }


    private void CompleteLevel(){
        Debug.Log("JEBAC DISA");
    }
}
