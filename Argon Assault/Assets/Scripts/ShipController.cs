using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ShipController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m/s")][SerializeField] float controlSpeed = 10f;   //우주선의 움직이는 속도
    [Tooltip("In m")] [SerializeField] float xRange = 8f;   //수직 이동 범위
    [Tooltip("In m")] [SerializeField] float yRange = 5.5f; //수평 이동 범워
    [SerializeField] GameObject[] guns; //레이저건 오브젝트 배열

    [Header("Rotation")]
    [SerializeField] float positionPitchFactor = -5f;   //y 포지션에 따른 x축 회전 보정
    [SerializeField] float controlPitchFactor = -20f;   //수직 이동 시 기울이는 정도
    [SerializeField] float positionYawFactor = 6.5f;    //x 포지션에 따른 y축 회전 보정
    [SerializeField] float controlRollFactor = -20f;    //수평 이동 시 기울이는 정도

    float xAxis, yAxis; //현재 x축과 y축
    bool isDestroyed = false;   //우주선이 파괴되었는지 확인

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed)
        {
            ShipMovement();
            ShipRotation();
            ShipAttack();
        }
    }

    private void ShipMovement()
    {
        xAxis = CrossPlatformInputManager.GetAxis("Horizontal");    //cross platform 입력 지원
        float xOffset = xAxis * controlSpeed * Time.deltaTime;  //한번에 x축으로 움직이는 정도
        float localXPos = transform.localPosition.x + xOffset;  //현재 위치에서 offset만큼 이동하기
        float clampedXpos = Mathf.Clamp(localXPos, -xRange, xRange);    //화면 범위 안에서 움직이도록 clamp 사용하기

        yAxis = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yAxis * controlSpeed * Time.deltaTime;
        float localYPos = transform.localPosition.y + yOffset;
        float clampedYpos = Mathf.Clamp(localYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXpos, clampedYpos, transform.localPosition.z); //위치 변경
    }

    private void ShipRotation()
    {
        float positionForPitch = transform.localPosition.y * positionPitchFactor; //y축에 따라 x축 회전시키기
        float controlForPitch = yAxis * controlPitchFactor; //움직일 때 현재 y축에 따라 x축 기울이기
        float pitch = positionForPitch + controlForPitch;   //x축 회전 시키기

        float yaw = transform.localPosition.x * positionYawFactor;  //x축에 따라 y축 회전시키기

        float controlForRoll = xAxis * controlRollFactor;   //움직일 때 현재 x축에 따라 z축 기울이기
        float roll = controlForRoll;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ShipAttack()
    {
        //공격 입력을 받으면 Gun particle On
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);   
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            //particle 효과를 자연스럽게 On/Off하기 위해 emission On/Off하기
            var gunEmission = gun.GetComponent<ParticleSystem>().emission;
            gunEmission.enabled = isActive;
        }
    }

    //CollisionHandler에서 호출
    private void OnShipDestroyed()
    {
        isDestroyed = true;
    }

}
