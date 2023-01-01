using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;
    // Start is called before the first frame update
    public void Awake()
    {
        _scoreText.text = GameManager.Instance.Score.ToString("d2");
    }

    public void Start()
    {
        
    }
    public void Setup()
    {

    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
