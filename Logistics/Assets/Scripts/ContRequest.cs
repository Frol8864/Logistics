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
            new Request(0, 0, "Шкаф", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(1, 1, "Стол", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(2, 2, "Стул", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(3, 3, "Холодильник", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(4, 4, "Дверь", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(5, 5, "Вешалки", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
            new Request(6, 6, "Бутылки", 30, 10, "Саратов", "12.05.2025", "10:00", "Москва", "13.05.2025", "15:00", 
                0, 2, statusRequest.work), 
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

    public List<Request> GetRequests(User user){
        List<Request> _requests = new List<Request>();
        switch (user.role) {
            case role.logist:
                _requests = requests;
            break;
            case role.driver:
                for(int i = 0; i < requests.Count; i++) {
                    if(requests[i].idDriver == user.idUser) _requests.Add(requests[i]);
                }
            break;
            case role.carrier:
                for(int i = 0; i < requests.Count; i++) {
                    if(requests[i].idCarrier == user.idUser) _requests.Add(requests[i]);
                }
            break;
        }
        return _requests;
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
