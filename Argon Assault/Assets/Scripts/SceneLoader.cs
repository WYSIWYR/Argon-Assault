using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Start/Restart 버튼을 누를시 GameScene 실행하기
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    //Exit 버튼을 누르면 프로그램 종료하기
    public void OnClickExit()
    {
        Application.Quit();
    }

    //게임이 끝나거나 우주선이 파괴되었을 때 EndScene 실행하기
    public void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }
}
