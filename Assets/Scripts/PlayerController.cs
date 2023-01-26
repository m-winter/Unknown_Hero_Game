using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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
  
    
    void Start()
    {        
        player = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        directionX = Input.GetAxis("Horizontal");

        player.velocity = new Vector2(directionX * speed, player.velocity.y);

        if(Input.GetButtonDown("Jump") && IsPlayerTouchingGround()){
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            jumpSoundEffect.Play();
        }

        UpdatePlayerAnimationOnMovement();
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
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}