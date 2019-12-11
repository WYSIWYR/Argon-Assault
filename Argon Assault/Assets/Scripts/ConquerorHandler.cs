using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConquerorHandler : MonoBehaviour
{
    //Scene이 시작될 때 Conquerer를 안보이게 하고 일정 시간이 지난뒤에 보이게 하기
    //이후 다시 일정 시간이 지나면 안보이게하기
    // Start is called before the first frame update
    void Start()
    {
        DisableConqueror();
        Invoke("SuprisePlayer", 9.76f);
        Invoke("DisableConqueror", 15f);
    }

    private void SuprisePlayer()
    {
        gameObject.SetActive(true);
    }

    private void DisableConqueror()
    {
        gameObject.SetActive(false);
    }
}
