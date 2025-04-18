using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Request : MonoBehaviour
{
    public int idRequest;
    public int numberRequest;
    public string nameCargo;
    public int weight;
    public int volume;
    public string addressFrom;
    public string dateFrom;
    public string timeFrom;
    public string addressTo;
    public string dateTo;
    public string timeTo;
    public int idDriver;
    public int idCarrier;
    public statusRequest statusRequest;

    public Request(int _idRequest, int _numberRequest, string _nameCargo, int _weight, int _volume,
                    string _addressFrom, string _dateFrom, string _timeFrom, string _addressTo, string _dateTo, string _timeTo,
                    int idDriver, int idCarrier, statusRequest statusRequest){
        idRequest = _idRequest;
        numberRequest = _numberRequest;
        nameCargo = _nameCargo;
        weight = _weight;
        volume = _volume;
        addressFrom = _addressFrom;
        dateFrom = _dateFrom;
        timeFrom = _timeFrom;
        addressTo = _addressTo;
        dateTo = _dateTo;
        timeTo = _timeTo;
        this.idDriver = idDriver;
        this.idCarrier = idCarrier;
        this.statusRequest = statusRequest;
    }

    public string GetStatus(){
        if(statusRequest == statusRequest.work) return "В работе";
        if(statusRequest == statusRequest.end) return "Выполнена";

        return "";
    }
}

public enum statusRequest{
    work,
    end,
}
