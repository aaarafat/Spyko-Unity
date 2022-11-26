using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{

    TextMeshPro textMesh;
    List<Color> colors;

    SpriteRenderer _sprite;
    Color targetColor;
    int _ind;
    int _size;
    float _alpha;
    private void Awake()
    {
        
        textMesh = GetComponentInChildren<TextMeshPro>();
        colors = GameManager.Instance.colors;
        _sprite = GetComponentInChildren<SpriteRenderer>();
        GameManager.OnScoreUpdate += GameManager_OnScoreUpdate;
        GameManager.OnColorUpdate += GameManager_OnColorUpdate;
        GameManager.OnGameStateChange += GameManager_OnGameStateChange;

    }

    private void OnDestroy()
    {
        GameManager.OnScoreUpdate -= GameManager_OnScoreUpdate;
        GameManager.OnColorUpdate -= GameManager_OnColorUpdate;
        GameManager.OnGameStateChange -= GameManager_OnGameStateChange;
    }
    private void GameManager_OnGameStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Death) targetColor.a = 0; 
    }

    private void GameManager_OnColorUpdate()
    {
        //throw new System.NotImplementedException();
        _ind++;
        _ind %= _size;
        Debug.Log(colors[_ind]);
      
    }

    private void GameManager_OnScoreUpdate(int score)
    {
        textMesh.text = score.ToString("d2");
        targetColor.a = _alpha;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetColor = _sprite.color;
        _alpha = targetColor.a;
        Debug.Log("start: "+ targetColor);
        targetColor.a = 0;
        _sprite.color = targetColor;
        textMesh.text = "00";
        _size = colors.Capacity;
        for (int i = 0; i < colors.Capacity; i++)
        {
            colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b);
        }
        textMesh.color =colors[0];

    }

    // Update is called once per frame
    void Update()
    {
       textMesh.color = Color.Lerp(textMesh.color, colors[_ind], Time.fixedDeltaTime);
        _sprite.color = Color.Lerp(_sprite.color, targetColor, Time.fixedDeltaTime);
    }

}
