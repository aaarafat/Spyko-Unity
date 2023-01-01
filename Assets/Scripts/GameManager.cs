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
    public int Score {  get; private set; }
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
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
        
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
    }
    public void HandleTouchWall()
    {
        NumberOfActiveSpikes = Mathf.Min(Score / 5 + 1,8);
        Score++;
        OnScoreUpdate?.Invoke(Score);
        if (Score > 0 && Score % 5 == 0) OnColorUpdate?.Invoke();


    }
}
