using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    public void AddToScore(int value){
        score += value;
    }
}
