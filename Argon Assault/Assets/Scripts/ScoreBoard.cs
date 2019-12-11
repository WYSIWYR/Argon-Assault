using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

    int score;  //점수를 저장하는 변수
    Text scoreText; //UI에 점수를 보여줄 Text

    // Start is called before the first frame update
    void Start()
    {
        //UI에 있는 Text 가져오기
        //점수를 Text에 String으로 변환해 표시하기
        scoreText = GetComponent<Text>();
        scoreText.text = score.ToString();
    }

    //EnemyHandler의 HitProcess 호출된다
    //hitScore만큼 점수를 추가한뒤 UI에 표시한다.
    public void ScoreHit(int hitScore)
    {
        score += hitScore;
        scoreText.text = score.ToString();
    }
}
