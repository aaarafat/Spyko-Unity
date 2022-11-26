using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    Camera _camera;
    List<Color> colors;
    int _ind;
    int _size;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        colors = GameManager.Instance.colors;
        GameManager.OnColorUpdate += GameManager_OnColorUpdate;
    }

    private void OnDestroy()
    {
        GameManager.OnColorUpdate -= GameManager_OnColorUpdate;
    }
    private void GameManager_OnColorUpdate()
    {
        _ind++;
        _ind %= _size;
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera.backgroundColor = colors[0];
        _size = colors.Capacity;
    }

    // Update is called once per frame
    void Update()
    {
        _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, colors[_ind], Time.fixedDeltaTime);
    }
}
