using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpSpeed = 10f;

    private SpriteRenderer sprite;
    private float directionX = 0f;

    private Rigidbody2D player;
    private Animator playerAnimator;

    private enum MovementState {idle, running, jumping, falling};

    [SerializeField] private AudioSource jumpSoundEffect;

    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpableGround;

    
    [SerializeField] private float speedBoostAmount = 5f;
    [SerializeField] private float speedBoostTime = 0f;
    [SerializeField] private bool isSpeedBoostActive = false;

    [SerializeField] private float jumpBoostAmount = 5f;
    [SerializeField] private float jumpBoostTime = 0f;
    [SerializeField] private bool isJumpBoostActive = false;

    [SerializeField] private float gravityChangeBoostTime = 0f;
    [SerializeField] private bool isGravityChangeBoostActive = false; 

    [SerializeField] private Image[] currentBoosts;
    [SerializeField] private Sprite speedBoostImage;
    [SerializeField] private Sprite jumpBoostImage;
    [SerializeField] private Sprite gravityBoostImage;
    
    void Start()
    {        
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        ResetBoostsImages();
    }

    void Update()
    {
        UpdateTimersForActiveBoosts();

        directionX = Input.GetAxis("Horizontal");
        if(isSpeedBoostActive != true){
            player.velocity = new Vector2(directionX * speed, player.velocity.y);
        }else{
            player.velocity = new Vector2(directionX * (speed+speedBoostAmount), player.velocity.y);

        }
        if(Input.GetButtonDown("Jump") && IsPlayerTouchingGround()){
            if(isGravityChangeBoostActive && jumpSpeed > 0f){
                jumpSpeed = jumpSpeed * -1;
                jumpBoostAmount = jumpBoostAmount * -1;
            }else if(!isGravityChangeBoostActive && jumpSpeed < 0f){
                jumpSpeed = Mathf.Abs(jumpSpeed);
                jumpBoostAmount = Mathf.Abs(jumpBoostAmount);                
            }

            if(isJumpBoostActive != true){
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            }else{
                player.velocity = new Vector2(player.velocity.x, jumpSpeed + jumpBoostAmount);
            }            
            jumpSoundEffect.Play();
            CreateDust();
        }

        UpdatePlayerAnimationOnMovement();
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.CompareTag("Strawberry")){
            isSpeedBoostActive = true;
            ManageBoostIcons("Sprint", true);
        }else if(collider.gameObject.CompareTag("Orange")){
            isJumpBoostActive = true;
            ManageBoostIcons("Jump", true);
        }else if(collider.gameObject.CompareTag("Melon")){
            isGravityChangeBoostActive = true;
            ManageBoostIcons("Gravity", true);
            ChangePlayerGravity();
        }
    }


    public void UpdatePlayerAnimationOnMovement(){
        MovementState state;

        if(directionX > 0f){
            state = MovementState.running;
            sprite.flipX = false;
        }else if(directionX < 0f){
            state = MovementState.running;
            sprite.flipX = true;
        }else{
            state = MovementState.idle;
        }

        if(player.velocity.y > .1f){
            state = MovementState.jumping;
        }else if(player.velocity.y < -.1f){
            state = MovementState.falling;
        }

        playerAnimator.SetInteger("state", (int)state);
    }

    private bool IsPlayerTouchingGround(){
        if(isGravityChangeBoostActive){
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, .1f, jumpableGround);
        }else{
             return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        }
    }

    private void CreateDust(){
        dust.Play();
    }

    private void ChangePlayerGravity(){
        if(player.transform.transform.localScale == new Vector3(1, 1, 1)){
            player.gravityScale = -2f;
            player.transform.transform.localScale += new Vector3(0, -2, 0);
        }        
    }

    private void UpdateTimersForActiveBoosts(){
        if(isJumpBoostActive){
            jumpBoostTime += Time.deltaTime;
            if(jumpBoostTime >=5f){
                jumpBoostTime = 0;
                isJumpBoostActive = false;
                ManageBoostIcons("Jump", false);
            }
        }
        if(isSpeedBoostActive){
            speedBoostTime += Time.deltaTime;
            if(speedBoostTime >= 5){
                isSpeedBoostActive = false;
                speedBoostTime = 0;
                ManageBoostIcons("Sprint", false);
            }
        }

        if(isGravityChangeBoostActive){
            gravityChangeBoostTime += Time.deltaTime;
            if(gravityChangeBoostTime >= 5f){
                isGravityChangeBoostActive = false;
                gravityChangeBoostTime = 0;
                player.transform.transform.localScale += new Vector3(0, 2, 0);
                player.gravityScale = 2f;
                ManageBoostIcons("Gravity", false);
            }
        }        

    }

    public void ResetBoostsImages(){
        for(int i = 0; i < currentBoosts.Length; i++){
            currentBoosts[i].enabled = false;
        }
    }

    public void ManageBoostIcons(string boostName, bool visibilityValue){
        for(int i = 0; i < currentBoosts.Length; i++){
            if(currentBoosts[i].name == boostName){
               currentBoosts[i].enabled = visibilityValue; 
            }
        }
    }


}