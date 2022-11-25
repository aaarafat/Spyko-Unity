using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    Camera _camera;
    [SerializeField] List<Color> colors;
    int _ind;
    float _curr;
    int _size;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _camera.backgroundColor = colors[0];
        GameManager.OnColorUpdate += GameManager_OnColorUpdate;
    }

    private void GameManager_OnColorUpdate()
    {
        Debug.Log(_camera.backgroundColor);
        _ind++;
        _ind %= _size;
        _curr = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        _size = colors.Capacity;
    }

    // Update is called once per frame
    void Update()
    {
        _curr = Mathf.Lerp(_curr, 1, Time.fixedDeltaTime * .1f);
        _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, colors[_ind], Time.fixedDeltaTime);
    }
}
