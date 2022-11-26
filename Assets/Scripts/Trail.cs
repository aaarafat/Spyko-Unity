using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    //[SerializeField] Player _player;
    [SerializeField] GameObject _trail;
    public bool Emit;
    [SerializeField] float _timeBetween;
    float _pastTime;
    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Emit) return;

        if(_pastTime <=0)
        {
            _pastTime = _timeBetween;
            GameObject gameObject = Instantiate<GameObject>(_trail,transform.position,Quaternion.identity);
            Destroy(gameObject, .5f); 
        }
        _pastTime -= Time.deltaTime;
    }
}
