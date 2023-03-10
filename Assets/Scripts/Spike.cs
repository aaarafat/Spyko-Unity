using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float speed;
    Vector2 targetPosition;
    Vector2 intialPosition;
    Vector2 currPosition;

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
        transform.position = Vector3.Lerp(transform.position, currPosition, Time.deltaTime * 10);
    }
    public void Activate()
    {
        currPosition = (currPosition != targetPosition) ? targetPosition : intialPosition;
    }

    public void UpdatePosition(Vector2 position)
    {
        transform.position = position;
        intialPosition = transform.position;
        targetPosition = new Vector2(transform.position.x + transform.right.x, transform.position.y);
        currPosition = intialPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.Death);
    }
}
