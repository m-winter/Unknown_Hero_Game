using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    public static GameStatsController gameStatsController {get; private set;}
  
    public int playerHealth = 5;


    void Awake() {
        if(gameStatsController != null && gameStatsController != this){
            Destroy(this);
        }else{
            gameStatsController = this;
        }    
    }
}
