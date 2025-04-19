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
    [SerializeField] ContUser contUser;
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
    [SerializeField] Text infoDriver;
    [SerializeField] Text infoCarrier;
    [SerializeField] GameObject fon;

    [SerializeField] GameObject popUpUsers;
    [SerializeField] List<BlockUser> blockUsers;
    [SerializeField] Text countPages;

    private List<User> users;
    private int indexPage;
    private role role;

    public User driver;
    public bool isDriver;
    public User carrier;
    public bool isCarrier;

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
        infoDriver.text = "";
        infoCarrier.text = "";
        errorInfo.text = "";
        fon.SetActive(flag);
        ClickOpenClosePopUpUsers(false, role.driver);
        isDriver = false;
        isCarrier = false;
        UpdateInfo();
    }

    public void ClickCreate(){
        if(!isDriver || !isCarrier) return;
        
        Request request = new Request(
            contRequest.requests.Count, contRequest.requests.Count, nameCargo.text, Int32.Parse(weight.text), Int32.Parse(volume.text), 
            addressFrom.text, dateFrom.text, timeFrom.text, addressTo.text, dateTo.text, timeTo.text,
            driver.idUser, carrier.idUser, statusRequest.work
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

    public void UpdateInfo(){
        if(isDriver){
            infoDriver.text = driver.family + " " + driver.nameUser + " " + driver.nameFather + ", " + driver.car.nameCar + " " + driver.car.number + " " + driver.car.vin;
        } else {
            infoDriver.text = "";
        }
        if(isCarrier){
            infoCarrier.text = carrier.legal.nameLegal + " " + carrier.legal.inn + " " + carrier.legal.kpp;
        } else {
            infoCarrier.text = "";
        }
    }

    public void UpdateUser(User user, role role){
        if(role == role.driver){
            isDriver = true;
            driver = user;
        }
        if(role == role.carrier){
            isCarrier = true;
            carrier = user;
        }
        ClickOpenClosePopUpUsers(false, role.driver);
        UpdateInfo();
    }

    public void OpenUserPopUp(int idRole){
        ClickOpenClosePopUpUsers(true, idRole == 0 ? role.driver : role.carrier);
    }

    public void ClickOpenClosePopUpUsers(bool flag, role role){
        if(role == role.driver && !isCarrier && flag) return;
        popUpUsers.SetActive(flag);
        if(!flag) return;
        indexPage = 0;
        this.role = role;
        users = contUser.GetListUser(role, carrier);
        for(int i = 0; i < blockUsers.Count; i++) {
            blockUsers[i].Clear();
        }
        for(int i = indexPage * blockUsers.Count; i < (indexPage + 1) * blockUsers.Count && i < users.Count; i++) {
            blockUsers[i % blockUsers.Count].AddUser(users[i], role);
        }
        countPages.GetComponent<Text>().text = (indexPage + 1).ToString() + " / " 
                + (users.Count / blockUsers.Count 
                    + (users.Count % blockUsers.Count == 0 ? 0 : 1)).ToString();
    }

    public void ChangeIndexPage(int x){
        if(indexPage + x < 0 || indexPage + x + 1 > users.Count / blockUsers.Count 
                + (users.Count % blockUsers.Count == 0 ? 0 : 1)) return;
        indexPage+=x;
        for(int i = 0; i < blockUsers.Count; i++) {
            blockUsers[i].Clear();
        }
        for(int i = indexPage * blockUsers.Count; i < (indexPage + 1) * blockUsers.Count && i < users.Count; i++) {
            blockUsers[i % blockUsers.Count].AddUser(users[i], role);
        }
        countPages.GetComponent<Text>().text = (indexPage + 1).ToString() + " / " 
                + (users.Count / blockUsers.Count 
                    + (users.Count % blockUsers.Count == 0 ? 0 : 1)).ToString();
    }
}
