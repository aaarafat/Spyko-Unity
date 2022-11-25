using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChange;
    public static event Action OnColorUpdate;
    public static event Action<int> OnScoreUpdate;
    [SerializeField] public  List<Color> colors;
    int _score;
    int _coins;
    public int NumberOfActiveSpikes;
    public enum GameState
    {
        Menu,
        RightWall,
        LeftWall,
        Death
    }

    private void Awake()
    {
        NumberOfActiveSpikes = 0;
        Instance = this;
        
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        //NumberOfActiveSpikes = Mathf.Min(8, NumberOfActiveSpikes + 1);
        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.RightWall:
                break;
            case GameState.LeftWall:
                break;
            case GameState.Death:
                HandleDeath();
                break;
            default:
                break;
        };
        OnGameStateChange?.Invoke(newState);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Menu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDeath() {
        Debug.Log("GameManger Death");
    }
    public void HandleTouchWall()
    {
        NumberOfActiveSpikes = Mathf.Min(_score / 5 + 1,8);
        _score++;
        OnScoreUpdate?.Invoke(_score);
        if (_score > 0 && _score % 5 == 0) OnColorUpdate?.Invoke();
        Debug.Log("Wall Touched, Active: " + NumberOfActiveSpikes+ " --- Score : "+ _score);


    }
}
