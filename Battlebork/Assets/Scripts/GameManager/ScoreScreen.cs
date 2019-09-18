using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {

    public Text score;
    public Text bones;
    public Text finalScore;


	// Use this for initialization
	void Start () {
        score.text = GameManager.totalScore.ToString();
        bones.text = GameManager.boneCount.ToString();
        if(GameManager.boneCount == 0)
        {
            finalScore.text = (GameManager.totalScore).ToString();
        }
        else
        {
            finalScore.text = (GameManager.totalScore * GameManager.boneCount).ToString();
        }
      
	}
	
	// Update is called once per frame
	void Update () {

	}

   
}
