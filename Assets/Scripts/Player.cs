using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    [SerializeField] float speed;
    [SerializeField] float thrust;
    [SerializeField] float maxThrustSpeed;
    bool flap;
    Vector2 velocity;
    Vector2 flapVelocity;
    Vector3 rotator = new Vector3(0, 180, 0);
    // Start is called before the first frame update
    Orientation _orientation;
    public enum Orientation
    {
        Left, 
        Right
    }
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = transform.right;
        velocity *= speed;
        flapVelocity = transform.up * thrust;
        _orientation = Orientation.Right;
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;
    }

    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Menu:
                break;
            case GameManager.GameState.RightWall:
                if (_orientation != Orientation.Right)
                {
                    Flip();
                }
                break;
            case GameManager.GameState.LeftWall:
                if (_orientation != Orientation.Left)
                {
                    Flip();
                }
                break;
            case GameManager.GameState.Death:
                break;
            default:
                break;
        }
    }

        // Update is called once per frame
        void Update()
    {
        if (Input.GetButtonDown("Jump")) flap = true ;
        
        animator.SetBool("Flapping", rb2d.velocity.y > 0);
    }
    private void FixedUpdate()
    {
        velocity.y = rb2d.velocity.y;
        if (flap)
        {
            Flap();
            flap = false;
        }
        rb2d.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Body entered");
        //transform.Rotate(rotator);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Body exited");
        //
    }
    public void Flip()
    {
        transform.Rotate(rotator);
        velocity = transform.right;
        velocity *= speed;
        if (_orientation == Orientation.Left) _orientation = Orientation.Right;
        else _orientation = Orientation.Left;
    }
    void Flap()
    {
        velocity.y = maxThrustSpeed;
    }
    
}