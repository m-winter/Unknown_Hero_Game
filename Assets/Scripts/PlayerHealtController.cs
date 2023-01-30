using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealtController : MonoBehaviour
{
    [SerializeField] private AudioSource playerDieSound;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private AudioSource playerHitSound;
    private Animator animator;
    private Rigidbody2D player;

    [SerializeField] private int playerHearts = 5;
    [SerializeField] private int playerMaxHearts = 5;
    [SerializeField] private Image[] currentHearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        UpdateHearths();
        
    }



    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.CompareTag("Spike Trap")){
            PlayerTakeDamage(1);       
        } 

    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Apple")){
            PlayerHeal(1);       
        }
    }

    private void UpdateHearths(){
        if(playerHearts > playerMaxHearts){
            playerHearts = playerMaxHearts;
        }

        for(int i = 0 ; i < currentHearts.Length; i++){
            if(i < playerHearts){
                currentHearts[i].sprite = fullHeart;
            }else{
                currentHearts[i].sprite = emptyHeart;
            }

            if(i < playerMaxHearts){
                currentHearts[i].enabled = true;
            }else{
                currentHearts[i].enabled = false;
            }
        }
    }

    private void PlayerTakeDamage(int dmg){
        playerHearts = playerHearts - dmg;
        UpdateHearths();
        playerHitSound.Play();
        SpreadBlood();

        if(playerHearts <= 0){
            Die();
        }
    }
    private void PlayerHeal(int heal){
        playerHearts = playerHearts + heal;
        UpdateHearths();
        //playerHitSound.Play();
    }

    private void Die(){
        player.bodyType = RigidbodyType2D.Static;
        playerDieSound.Play();
        animator.SetTrigger("death");
    }
    
    private void RestartLife(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SpreadBlood(){
        blood.Play();
    }
}
