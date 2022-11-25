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
    int _deathKicks;
    Vector3 rotator = new Vector3(0, 180, 0);
    Orientation _orientation;
    public enum Orientation
    {
        Left, 
        Right
    }
    void Awake()
    {
        _deathKicks = 3;
        DeathKick = new Vector2(15, 15);
        _isAlive = true;
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb2d.gravityScale = 0;
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


    // Update is called once per frame
    void Update()
    {
        if (!_isAlive) return;
        if(GameManager.Instance.State == GameManager.GameState.Menu)
        {
            transform.position = Vector2.zero + Vector2.up * Mathf.Sin(Time.time * 2.0f) ;
            animator.SetBool("Flapping", true);
            if (Input.GetButtonDown("Jump"))
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.RightWall);
                rb2d.gravityScale = 3;
                if (Input.GetButtonDown("Jump")) flap = true;

            }
        }
        else
        { 
            if (Input.GetButtonDown("Jump")) flap = true;
            animator.SetBool("Flapping", rb2d.velocity.y > 0);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.State == GameManager.GameState.Menu) return;
        if (!_isAlive) return;
        velocity.y = rb2d.velocity.y;
        if (flap)
        {
            Flap();
            flap = false;
        }
        rb2d.velocity = velocity;
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

    private void HandleDeath()
    {
        if (_deathKicks <= 0) return;
        Debug.Log("Player Died");
        animator.SetBool("Dead", true);
        rb2d.velocity = Vector2.zero;
        float sign = Mathf.Sign(transform.position.x) > 0 ? -1 : 1;
        DeathKick.y = UnityEngine.Random.Range(5f, 15f);
        DeathKick.x = UnityEngine.Random.Range(5f, 15f);
        DeathKick.x *= sign;
        rb2d.freezeRotation = false;
        rb2d.AddTorque(UnityEngine.Random.Range(75f, 200f) * sign * -1);
        rb2d.AddForce(DeathKick, ForceMode2D.Impulse);
        //rb2d.velocity = DeathKick;
        _isAlive = false;
        _deathKicks--;
    }

}