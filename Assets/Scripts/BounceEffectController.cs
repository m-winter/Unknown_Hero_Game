using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffectController : MonoBehaviour
{
    [SerializeField] private float bounceForce =14f;

    private void OnCollisionEnter2D(Collision2D collider) {
        if(collider.gameObject.CompareTag("Player")){
            collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up *bounceForce, ForceMode2D.Impulse);

        }
    }
}
