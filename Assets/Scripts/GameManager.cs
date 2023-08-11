using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int score=0;
    [SerializeField] TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        UpdateScoreText();
    }

    public void AddToScore(int value){
        score += value;
        UpdateScoreText();
    }

    void UpdateScoreText(){
        scoreText.text = score.ToString();
    }
}
