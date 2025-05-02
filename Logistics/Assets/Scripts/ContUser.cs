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
    public List<User> users;
    [SerializeField] Text fioText;
    [SerializeField] Text roleText;
    [SerializeField] DriverPopUp driverPopUp;
    [SerializeField] LogistPopUp logistPopUp;
    [SerializeField] CarrierPopUp carrierPopUp;
    [SerializeField] CreateRequestPopUp createRequestPopUp;
    [SerializeField] CreateUserPopUp createUserPopUp;

    public void GenUser(){
        users = new List<User>{
            new User(0, "Райан", "Гослинг", "", "79675006885", role.driver, new Car("Geely", "12345678901234567", "A123AA123"), null, 2),
            new User(1, "Христофор", "Колумб", "", "79675006886", role.logist, null, null, 0),
            new User(2, "Джейсон", "Стэйтем", "", "79675006887", role.carrier, null, new Legal("Колхоз 40 лет без урожая", "123456789012", "0987654321"), 1),
            //new User(3, "Джейсон", "Стэйтем", "", "79675006888", role.carrier, null, new Legal("ООО Моя Оборона", "210987654321", "1234567890"), 1),
            //new User(4, "Доминик", "Торетто", "", "79675006889", role.driver, new Car("Dodge", "12345678901234567", "A123AA123"), null, 2),
        };
    }

    public void AuthUser(int idUser){
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

    public dataAnswerUser GetUser(int idUser){
        for(int i = 0; i < users.Count; i++) {
            if(idUser == users[i].idUser) {
                return new dataAnswerUser(true, users[i], "");;
            }
        }
        return new dataAnswerUser(false, null, "Пользователь не найден");
    }

    public List<User> GetListUser(role role, User user){
        List<User> _users = new List<User>();
        switch (role) {
            case role.carrier:
                for(int i = 0; i < users.Count; i++) {
                    if(users[i].role == role.carrier) _users.Add(users[i]);
                }
            break;
            case role.driver:
                for(int i = 0; i < users.Count; i++) {
                    if(users[i].role == role.driver && users[i].idUserCreate == user.idUser) _users.Add(users[i]);
                }
            break;
        }
        return _users;
    }

    public void ClickMain(){
        if(user.role == role.driver) {
            driverPopUp.ClickOpenClose(true);
        }
        if(user.role == role.logist) {
            driverPopUp.ClickOpenClose(false);
            createRequestPopUp.ClickOpenClose(false);
            createUserPopUp.ClickOpenClose(false);
            carrierPopUp.ClickOpenClose(false);
            logistPopUp.ClickOpenClose(true);
        }
        if(user.role == role.carrier) {
            driverPopUp.ClickOpenClose(false);
            createRequestPopUp.ClickOpenClose(false);
            createUserPopUp.ClickOpenClose(false);
            logistPopUp.ClickOpenClose(false);
            carrierPopUp.ClickOpenClose(true);
        }
    }
}

public class dataAnswerUser{
    public bool succes;
    public User user;
    public string error;
    public dataAnswerUser(bool _succes, User _user, string _error){
        succes = _succes;
        user = _user;
        error = _error;
    }
}