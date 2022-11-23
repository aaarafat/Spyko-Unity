using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpikesWall : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spikes;
    // Start is called before the first frame update

    [SerializeField]  bool flip;
    [Range(0,6)]
    [SerializeField] float offset = 4.5f;
    [Range(1, 3)]
    [SerializeField] float gap = 2;
    bool _isFlipped;
    Vector2 rotator = new Vector2(0, 180);
    bool _myTurn;
    [SerializeField] WallPosition _wallPosition;

    private enum WallPosition
    {
        Left,
        Right
    }

    private void Awake()
    {
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        if(_myTurn )
        {
            Flip();
            _myTurn = false;
        }
        else if(isItMyTurn(state))
        {
            _myTurn = true;
            Flip();
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
        if(_isFlipped != flip)
        {
            Flip();
            _isFlipped = flip;
        }
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
    void Flip()
    {
        foreach (GameObject spikeObject in spikes)
        {
            
            Spike spike = spikeObject.GetComponent<Spike>();
            spike.Activate();
        }
    }
    void Test()
    {
        foreach (GameObject spikeObject in spikes)
        {
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
