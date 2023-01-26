using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealtController : MonoBehaviour
{
    [SerializeField] private AudioSource playerDieSound;
    [SerializeField] private Text healthText;
    [SerializeField] private AudioSource playerHitSound;
    private Animator animator;
    private Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        healthText.text = "Health: " + GameStatsController.gameStatsController.playerHealth;
    }
    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.CompareTag("Spike Trap")){
            PlayerTakeDamage(1);
            Debug.Log(GameStatsController.gameStatsController.playerHealth);       
        }
    }

    private void PlayerTakeDamage(int dmg){
        GameStatsController.gameStatsController.playerHealth = GameStatsController.gameStatsController.playerHealth - dmg;
        healthText.text = "Health: " + GameStatsController.gameStatsController.playerHealth;
        playerHitSound.Play();
        
        if(GameStatsController.gameStatsController.playerHealth == 0){
            Die();
        }
    }
    private void PlayerHeal(int heal){
        GameStatsController.gameStatsController.playerHealth = GameStatsController.gameStatsController.playerHealth + heal;    
        }

    private void Die(){
        player.bodyType = RigidbodyType2D.Static;
        playerDieSound.Play();
        animator.SetTrigger("death");
    }
    
    private void RestartLife(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
