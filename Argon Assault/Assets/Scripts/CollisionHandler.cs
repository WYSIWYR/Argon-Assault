using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Load new scene delay")][SerializeField] float levelLoadDelay = 0.3f;  //다음 Scene을 호출할 때 가지는 대기시간
    [Tooltip("FX prefab on ship")] [SerializeField] GameObject destroyFX;   //우주선이 파괴될 때 실행할 Particle 가져오기
    SceneLoader sceneLoader;    //다음 Scene을 부르기 위해 SceneLoader 가져오기

    //Collider와 충돌하면 실행
    private void OnTriggerEnter(Collider other)
    {
        //Collider가 가진 tag가 GameEnd아니면 실행
        if (other.tag != "GameEnd")
        {
            ShipDestroyed();
            destroyFX.SetActive(true);
        }

        Invoke("GameEnd", levelLoadDelay);
    }

    //ShipController에 있는 우주선 파괴함수 불러오기
    private void ShipDestroyed()
    {
        SendMessage("OnShipDestroyed");

    }

    //EndScene 실행하기
    private void GameEnd()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadEndScene();
    }
}
