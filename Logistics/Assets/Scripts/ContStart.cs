using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ContStart : MonoBehaviour
{
    [SerializeField] ContAuth contAuth;
    [SerializeField] ContUser contUser;
    [SerializeField] ContRequest contRequest;

    private void Start() {
        contUser.GenUser();
        contRequest.GenRequsts();
        contAuth.StartAuth();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
