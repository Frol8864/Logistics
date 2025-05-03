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
    [SerializeField] ContUser contUser;
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
    [SerializeField] Text infoDriver;
    [SerializeField] Text infoCarrier;
    [SerializeField] Text status;
    [SerializeField] GameObject changeStatusRequest;

    [SerializeField] List<BlockRequest> blockRequests;
    [SerializeField] Text countPages;

    [SerializeField] Map map;

    private List<Request> requests;
    private int indexPage;
    private Request request;

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
        infoDriver.GetComponent<Text>().text = "";
        infoCarrier.GetComponent<Text>().text = "";
        status.GetComponent<Text>().text = "";
        changeStatusRequest.SetActive(false);
        fon.SetActive(flag);

        if(flag){
            indexPage = 0;
            requests = contRequest.GetRequests(contUser.user);
            for(int i = 0; i < blockRequests.Count; i++) {
                blockRequests[i].Clear();
            }
            for(int i = indexPage * blockRequests.Count; i < (indexPage + 1) * blockRequests.Count && i < requests.Count; i++) {
                blockRequests[i % blockRequests.Count].AddRequest(requests[i]);
            }
            countPages.GetComponent<Text>().text = (indexPage + 1).ToString() + " / " 
                + (requests.Count / blockRequests.Count 
                    + (requests.Count % blockRequests.Count == 0 ? 0 : 1)).ToString();
        }
    }

    public void ChangeIndexPage(int x){
        if(indexPage + x < 0 || indexPage + x + 1 > requests.Count / blockRequests.Count 
                + (requests.Count % blockRequests.Count == 0 ? 0 : 1)) return;
        indexPage+=x;
        requests = contRequest.GetRequests(contUser.user);
        for(int i = 0; i < blockRequests.Count; i++) {
            blockRequests[i].Clear();
        }
        for(int i = indexPage * blockRequests.Count; i < (indexPage + 1) * blockRequests.Count && i < requests.Count; i++) {
            blockRequests[i % blockRequests.Count].AddRequest(requests[i]);
        }
        countPages.GetComponent<Text>().text = (indexPage + 1).ToString() + " / " 
            + (requests.Count / blockRequests.Count 
                + (requests.Count % blockRequests.Count == 0 ? 0 : 1)).ToString();
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
            infoDriver.GetComponent<Text>().text = "";
            infoCarrier.GetComponent<Text>().text = "";
            status.GetComponent<Text>().text = "";
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
            status.GetComponent<Text>().text = dataAnswerSearch.request.GetStatus();
            dataAnswerUser dataAnswerUser = contUser.GetUser(dataAnswerSearch.request.idDriver);
            User user = dataAnswerUser.user;
            if(dataAnswerUser.succes){
                infoDriver.GetComponent<Text>().text = user.family + " " + user.nameUser + ", " + user.car.nameCar + " " + user.car.number + " " + user.car.vin;
            } else {
                infoDriver.GetComponent<Text>().text = dataAnswerUser.error;
            }
            dataAnswerUser = contUser.GetUser(dataAnswerSearch.request.idCarrier);
            user = dataAnswerUser.user;
            if(dataAnswerUser.succes){
                infoCarrier.GetComponent<Text>().text = user.family + " " + user.nameUser + ", " + user.legal.nameLegal + " " + user.legal.inn + " " + user.legal.kpp;
            } else {
                infoCarrier.GetComponent<Text>().text = dataAnswerUser.error;
            }
            errorInfo.GetComponent<Text>().text = "";
            map.ShowMapForRequest(dataAnswerSearch.request);
        }
    }
    public void ChangeStatuRequest(){
        request.statusRequest = request.statusRequest == statusRequest.work ? statusRequest.end : statusRequest.work;
        ShowRequest(request);
        ChangeIndexPage(0);
    }
    public void ShowRequest(Request request){
        this.request = request;
        nameCargo.GetComponent<Text>().text = request.nameCargo;
        weight.GetComponent<Text>().text = request.weight.ToString();
        volume.GetComponent<Text>().text = request.volume.ToString();
        addressFrom.GetComponent<Text>().text = request.addressFrom;
        dateFrom.GetComponent<Text>().text = request.dateFrom;
        timeFrom.GetComponent<Text>().text = request.timeFrom;
        addressTo.GetComponent<Text>().text = request.addressTo;
        dateTo.GetComponent<Text>().text = request.dateTo;
        timeTo.GetComponent<Text>().text = request.timeTo;
        status.GetComponent<Text>().text = request.GetStatus();
        dataAnswerUser dataAnswerUser = contUser.GetUser(request.idDriver);
        User user = dataAnswerUser.user;
        if(dataAnswerUser.succes){
            infoDriver.GetComponent<Text>().text = user.family + " " + user.nameUser + ", " + user.car.nameCar + " " + user.car.number + " " + user.car.vin;
        } else {
            infoDriver.GetComponent<Text>().text = dataAnswerUser.error;
        }
        dataAnswerUser = contUser.GetUser(request.idCarrier);
        user = dataAnswerUser.user;
        if(dataAnswerUser.succes){
            infoCarrier.GetComponent<Text>().text = user.family + " " + user.nameUser + ", " + user.legal.nameLegal + " " + user.legal.inn + " " + user.legal.kpp;
        } else {
            infoCarrier.GetComponent<Text>().text = dataAnswerUser.error;
        }
        errorInfo.GetComponent<Text>().text = "";
        if(contUser.user.role == role.logist){
            changeStatusRequest.SetActive(true);
        }
        map.ShowMapForRequest(request);
    }


}
