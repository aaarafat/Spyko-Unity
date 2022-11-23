using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpikesWall : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spikes;
    // Start is called before the first frame update


    [Range(0,6)]
    [SerializeField] float offset = 4.5f;
    [Range(1, 3)]
    [SerializeField] float gap = 2;

    Vector2 rotator = new Vector2(0, 180);
    bool _myTurn;
    [SerializeField] WallPosition _wallPosition;
    List<int> _currentActive;
    private enum WallPosition
    {
        Left,
        Right
    }

    private void Awake()
    {
        _currentActive = new List<int>();
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        
        if (_myTurn)
        {
            Activate();
            _myTurn = false;
        }
        else if (isItMyTurn(state))
        {
            GenerateRandomList();
            _myTurn = true;
            Activate();
        }
    }

    private void GenerateRandomList()
    {
        _currentActive.Clear();
        for (int i = 0; i < GameManager.Instance.NumberOfActiveSpikes; i++)
        {
            int rand = Random.Range(0, 8);
            while (_currentActive.Contains(rand))
            {
                rand = Random.Range(0, 8);
            }
            _currentActive.Add(rand);

        }
    }

    void Start()
    {
       
    }

    private void OnValidate()
    {
        UpdatePosition();
    }
    // Update is called once per frame
    void Update()
    {

    }

    void UpdatePosition()
    {
        float start = 12.5f - offset;
        Vector2 position = new Vector2(transform.position.x, start);
        foreach (GameObject spike in spikes)
        { 
            spike.transform.position = position;
            position.y -= gap;
        }
    }

    void Activate()
    {
        foreach (int i in _currentActive)
        {
            GameObject spikeObject = spikes[i];
            Spike spike =  spikeObject.GetComponent<Spike>();
            spike.Activate();
        }
    }

    
    bool isItMyTurn(GameManager.GameState state)
    {
        if (_wallPosition == WallPosition.Left && state == GameManager.GameState.LeftWall) { return true; }
        else if (_wallPosition == WallPosition.Right && state == GameManager.GameState.RightWall) { return true; }
        else return false;
    }
}
