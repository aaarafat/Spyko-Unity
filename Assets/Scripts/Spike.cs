using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 targetPosition;
    Vector2 intialPosition;
    Vector2 currPosition;
    float _curr;

    // Start is called before the first frame update
    private void Awake()
    {
        intialPosition = transform.position;
        targetPosition = new Vector2(transform.position.x + transform.right.x, transform.position.y);
        currPosition = intialPosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _curr = Mathf.Lerp(_curr, 1, Time.fixedDeltaTime * speed) ;
        transform.position = Vector3.Lerp(transform.position, currPosition, _curr);
    }
    public void Activate()
    {
        currPosition = (currPosition != targetPosition) ? targetPosition : intialPosition;
        _curr = 0;
    }

    public void UpdatePosition(Vector2 position)
    {
        transform.position = position;
        intialPosition = transform.position;
        targetPosition = new Vector2(transform.position.x + transform.right.x, transform.position.y);
        currPosition = intialPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.State == GameManager.GameState.Death) return;
        GameManager.Instance.UpdateGameState(GameManager.GameState.Death);
    }
}
