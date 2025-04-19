using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class BlockRequest : MonoBehaviour
{
    [SerializeField] Text numberRequest;
    [SerializeField] Text status;
    [SerializeField] Text nameCargo;
    [SerializeField] Text addressFrom;
    [SerializeField] Text addressTo;
    [SerializeField] GameObject empty;

    [SerializeField] DriverPopUp driverPopUp;

    private bool isRequst;
    private Request request;

    public void Clear(){
        empty.SetActive(true);
        isRequst = false;
    }

    public void AddRequest(Request request){
        empty.SetActive(false);
        isRequst = true;
        this.request = request;

        numberRequest.GetComponent<Text>().text = request.numberRequest.ToString();
        status.GetComponent<Text>().text = request.GetStatus();
        nameCargo.GetComponent<Text>().text = request.nameCargo.ToString();
        addressFrom.GetComponent<Text>().text = request.addressFrom.ToString();
        addressTo.GetComponent<Text>().text = request.addressTo.ToString();
    }

    private void OnMouseUp() {
        if(!isRequst) return;
        driverPopUp.ShowRequest(request);
    }
}
