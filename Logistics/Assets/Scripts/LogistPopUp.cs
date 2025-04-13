using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LogistPopUp : MonoBehaviour
{
    [SerializeField] GameObject fon;
    [SerializeField] DriverPopUp driverPopUp;
    [SerializeField] CreateRequestPopUp createRequestPopUp;

    public void ClickOpenClose(bool flag){
        fon.SetActive(flag);
    }

    public void ClickOpenListRequestPopUp(){
        ClickOpenClose(false);
        driverPopUp.ClickOpenClose(true);
    }

    public void ClickOpenCreateRequestPopUp(){
        ClickOpenClose(false);
        createRequestPopUp.ClickOpenClose(true);
    }
}
