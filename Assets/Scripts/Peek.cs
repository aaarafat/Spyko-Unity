using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peek : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Death)
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.HandleTouchWall();
        GameManager.GameState gameState = Mathf.Sign(collision.transform.position.x) > 0 ? GameManager.GameState.LeftWall : GameManager.GameState.RightWall;
        GameManager.Instance.UpdateGameState(gameState);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Peek exited");
    }
}
