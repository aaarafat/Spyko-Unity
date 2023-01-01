using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverHUD;
    [SerializeField] GameObject MainMenuHUD;
    bool _showGameOver;
    int _Counter;
    private bool _inGame;

    // Start is called before the first frame update
    private void Awake()
    {
        Player.OnPlayerDeath += Player_OnPlayerDeath;
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void Player_OnPlayerDeath()
    {
        Instantiate(GameOverHUD);
    }

    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        if(! _inGame && state!= GameManager.GameState.Menu)
        {
            MainMenuHUD.SetActive(false);
            _inGame = true;
        }
        switch (state)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.RightWall:
                break;
            case GameManager.GameState.LeftWall:
                break;
            case GameManager.GameState.Death:
                GameManager.OnGameStateChange -= GameManager_OnGameStateChange;
                break;
            default:
                break;
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
