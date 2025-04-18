using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ContRequest : MonoBehaviour
{
    public List<Request> requests;

    public void GenRequsts(){
        requests = new List<Request>{
            new Request(0, 101, "Шкаф", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0)
        };
    }

    public DataAnswerSearch GetRequestByNumber(int numberRequest){
        for(int i = 0; i < requests.Count; i++) {
            if(requests[i].numberRequest == numberRequest){
                return new DataAnswerSearch(true, requests[i], "");
            }
        }
        return new DataAnswerSearch(false, null, "Нет доступа к данной заявке");
    }
}

public class DataAnswerSearch{
    public bool succes;
    public Request request;
    public string error;
    public DataAnswerSearch(bool _succes, Request _request, string _error){
        succes = _succes;
        request = _request;
        error = _error;
    }
}
