using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] GameObject destroyFX;  //Enemy가 파괴될 때 실행할 Particle 가져오기
    [SerializeField] Transform parent;  //destroyFX가 생성될 때 hierarchy에서 관리할 부모 위치로 옮기기
    [SerializeField] int scorePerHit = 150; //유저 우주선의 레이저에 맞을 때마다 추가할 점수
    [SerializeField] int barrier = 3;   //유저 우주선의 레이저에 맞을 때마다 감소될 방어막

    ScoreBoard scoreBoard;  //UI에 점수를 출력하기 위해 ScoreBoard 가져오기

    // Start is called before the first frame update
    void Start()
    {
        //Trigger가 On인 BoxCollider가 없으면 추가
        if (gameObject.GetComponent<BoxCollider>() == null)
        {
            AddNonTriggerBoxCollider();
        }

        //ScoreBoard를 찾아 가져오기
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    //Particle이 Enemy와 충돌하면 실행
    private void OnParticleCollision(GameObject other)
    {
        HitProcess();
        if (barrier <= 0)
        {
            DestroyEnemy();
        }
    }

    //충돌시 점수추가하기
    private void HitProcess()
    {
        scoreBoard.ScoreHit(scorePerHit);
        barrier -= 1;
    }

    //barrier가 0이하가 되면 destroyFX 실행하고 Enemy를 파괴
    private void DestroyEnemy()
    {
        GameObject fx = Instantiate(destroyFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
