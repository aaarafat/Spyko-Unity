using System;
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
    [SerializeField] Vector2 DeathKick;
    bool _isAlive; 
    bool flap;
    Vector2 velocity;
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
        DeathKick = new Vector2(15, 15);
        _isAlive = true;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = transform.right;
        velocity *= speed;
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
                HandleDeath();
                break;
            default:
                break;
        }
    }

    private void HandleDeath()
    {
        Debug.Log("Player Died");
        animator.SetBool("Dead", true);
        float sign = Mathf.Sign(transform.position.x) > 0 ? -1 : 1;
        DeathKick.x *= sign;
        rb2d.freezeRotation = false;
        rb2d.AddTorque(100f * sign * -1);
        rb2d.AddForce(DeathKick, ForceMode2D.Impulse);
        //rb2d.velocity = DeathKick;
        _isAlive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAlive) return;
        if (Input.GetButtonDown("Jump")) flap = true ;
        
        animator.SetBool("Flapping", rb2d.velocity.y > 0);
    }
    private void FixedUpdate()
    {
        if (!_isAlive) return;
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