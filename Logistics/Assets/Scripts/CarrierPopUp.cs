using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CarrierPopUp : MonoBehaviour
{
    [SerializeField] GameObject fon;
    [SerializeField] DriverPopUp driverPopUp;
    [SerializeField] CreateUserPopUp createUserPopUp;

    public void ClickOpenClose(bool flag){
        fon.SetActive(flag);
    }

    public void ClickOpenListRequestPopUp(){
        ClickOpenClose(false);
        driverPopUp.ClickOpenClose(true);
    }

    public void ClickOpenCreateUserPopUp(){
        ClickOpenClose(false);
        createUserPopUp.ClickOpenClose(true);
    }
}
