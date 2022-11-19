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
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        velocity = transform.right;
        velocity *= speed;
        flapVelocity = transform.up * thrust;
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
    }
    void Flap()
    {
        velocity.y = maxThrustSpeed;
    }
}
