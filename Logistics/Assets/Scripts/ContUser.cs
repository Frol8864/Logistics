using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ContUser : MonoBehaviour
{
    public User user;
    private List<User> users;
    [SerializeField] Text fioText;
    [SerializeField] Text roleText;
    [SerializeField] DriverPopUp driverPopUp;
    [SerializeField] LogistPopUp logistPopUp;
    [SerializeField] CreateRequestPopUp createRequestPopUp;

    public void GenUser(){
        users = new List<User>{
            new User(0, "Райан", "Гослинг", "", "79675006885", role.driver, new Car("Geely", "12345678901234567", "A123AA123")),
            new User(1, "Христофор", "Колумб", "", "79675006886", role.logist, null),
            new User(2, "Джейсон", "Стэйтем", "", "79675006887", role.carrier, null),
        };
    }

    public void GetUser(int idUser){
        for(int i = 0; i < users.Count; i++) {
            if(idUser == users[i].idUser) {
                user = users[i];
                fioText.GetComponent<Text>().text = user.family + " " + user.nameUser + " " + user.nameFather;
                roleText.GetComponent<Text>().text = user.role == role.driver ? "Водитель" : user.role == role.carrier ? "Перевозчик" : user.role == role.logist ? "Логист" : "";
                ClickMain();
                return;
            }
        }
    }

    public void ClickMain(){
        if(user.role == role.driver) {
            driverPopUp.ClickOpenClose(true);
        }
        if(user.role == role.logist) {
            driverPopUp.ClickOpenClose(false);
            createRequestPopUp.ClickOpenClose(false);
            logistPopUp.ClickOpenClose(true);
        }
    }
}
