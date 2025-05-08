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
            new Request(1, 1, "Мука", 20, 82, "г. Москва, 1-я Магистральная ул., 14", "23.04.2025", "08:00", "г. Саратов, ул. Новоастраханское шоссе 41А", "25.04.2025", "09:00", 
                1, 0, statusRequest.end), 
            new Request(2, 2, "Комплектующие", 4, 6, "г. Путилково, ул. Путилковское шоссе стр112А", "29.04.2025", "10:00", "Саратов, пр-т им. 50 лет Октября, д. 108", "30.04.2025", "09:00", 
                1, 0, statusRequest.end), 
            new Request(3, 3, "Оборудование в ящиках", 2, 10, "Г.Санкт-Петербург,ул.Бабушкина, д.123, литер В, Корп. 11", "22.05.2025", "16:00", "Саратов, пр-т им. 50 лет Октября, д. 108", "26.05.2025", "10:00", 
                1, 0, statusRequest.work), 
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
