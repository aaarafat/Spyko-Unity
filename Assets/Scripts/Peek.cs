using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peek : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Peek entered");
        player.Flip();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Peek exited");
    }
}
