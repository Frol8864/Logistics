using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;
public class CreateRequestPopUp : MonoBehaviour
{
    [SerializeField] ContRequest contRequest;
    [SerializeField] InputField nameCargo;
    [SerializeField] InputField weight;
    [SerializeField] InputField volume;
    [SerializeField] InputField addressFrom;
    [SerializeField] InputField dateFrom;
    [SerializeField] InputField timeFrom;
    [SerializeField] InputField addressTo;
    [SerializeField] InputField dateTo;
    [SerializeField] InputField timeTo;
    [SerializeField] Text errorInfo;
    [SerializeField] GameObject fon;

    public User driver;
    public bool isDriver;

    public void ClickOpenClose(bool flag){
        nameCargo.text = "";
        weight.text = "";
        volume.text = "";
        addressFrom.text = "";
        dateFrom.text = "";
        timeFrom.text = "";
        addressTo.text = "";
        dateTo.text = "";
        timeTo.text = "";
        errorInfo.text = "";
        fon.SetActive(flag);
        isDriver = false;
    }

    public void ClickCreate(){
        if(!isDriver) return;
        
        Request request = new Request(
            contRequest.requests.Count, contRequest.requests.Count, nameCargo.text, Int32.Parse(weight.text), Int32.Parse(volume.text), 
            addressFrom.text, dateFrom.text, timeFrom.text, addressTo.text, dateTo.text, timeTo.text,
            driver.idUser
        );
        contRequest.requests.Add(request);
        nameCargo.text = "";
        weight.text = "";
        volume.text = "";
        addressFrom.text = "";
        dateFrom.text = "";
        timeFrom.text = "";
        addressTo.text = "";
        dateTo.text = "";
        timeTo.text = "";
        errorInfo.text = "Заявка под номером " + request.numberRequest + " создана";
    }
}
