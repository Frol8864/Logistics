using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class DriverPopUp : MonoBehaviour
{
    [SerializeField] ContRequest contRequest;
    [SerializeField] InputField numberRequest;
    [SerializeField] Text nameCargo;
    [SerializeField] Text weight;
    [SerializeField] Text volume;
    [SerializeField] Text addressFrom;
    [SerializeField] Text dateFrom;
    [SerializeField] Text timeFrom;
    [SerializeField] Text addressTo;
    [SerializeField] Text dateTo;
    [SerializeField] Text timeTo;
    [SerializeField] Text errorInfo;
    [SerializeField] GameObject fon;

    public void ClickOpenClose(bool flag){
        numberRequest.text = "";
        nameCargo.GetComponent<Text>().text = "";
        weight.GetComponent<Text>().text = "";
        volume.GetComponent<Text>().text = "";
        addressFrom.GetComponent<Text>().text = "";
        dateFrom.GetComponent<Text>().text = "";
        timeFrom.GetComponent<Text>().text = "";
        addressTo.GetComponent<Text>().text = "";
        dateTo.GetComponent<Text>().text = "";
        timeTo.GetComponent<Text>().text = "";
        errorInfo.GetComponent<Text>().text = "";
        fon.SetActive(flag);
    }

    public void ClickSearch(){
        DataAnswerSearch dataAnswerSearch = contRequest.GetRequestByNumber(Int32.Parse(numberRequest.text));
        numberRequest.text = "";
        if(!dataAnswerSearch.succes){
            nameCargo.GetComponent<Text>().text = "";
            weight.GetComponent<Text>().text = "";
            volume.GetComponent<Text>().text = "";
            addressFrom.GetComponent<Text>().text = "";
            dateFrom.GetComponent<Text>().text = "";
            timeFrom.GetComponent<Text>().text = "";
            addressTo.GetComponent<Text>().text = "";
            dateTo.GetComponent<Text>().text = "";
            timeTo.GetComponent<Text>().text = "";
            errorInfo.GetComponent<Text>().text = dataAnswerSearch.error;
        } else {
            nameCargo.GetComponent<Text>().text = dataAnswerSearch.request.nameCargo;
            weight.GetComponent<Text>().text = dataAnswerSearch.request.weight.ToString();
            volume.GetComponent<Text>().text = dataAnswerSearch.request.volume.ToString();
            addressFrom.GetComponent<Text>().text = dataAnswerSearch.request.addressFrom;
            dateFrom.GetComponent<Text>().text = dataAnswerSearch.request.dateFrom;
            timeFrom.GetComponent<Text>().text = dataAnswerSearch.request.timeFrom;
            addressTo.GetComponent<Text>().text = dataAnswerSearch.request.addressTo;
            dateTo.GetComponent<Text>().text = dataAnswerSearch.request.dateTo;
            timeTo.GetComponent<Text>().text = dataAnswerSearch.request.timeTo;
            errorInfo.GetComponent<Text>().text = "";
        }
    }
}
