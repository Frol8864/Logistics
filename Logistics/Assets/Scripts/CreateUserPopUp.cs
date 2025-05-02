using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CreateUserPopUp : MonoBehaviour
{
    [SerializeField] ContUser contUser;
    [SerializeField] ContAuth contAuth;
    [SerializeField] InputField nameUser;
    [SerializeField] InputField family;
    [SerializeField] InputField nameFather;
    [SerializeField] InputField login;
    [SerializeField] InputField password;
    [SerializeField] Text errorInfo;
    [SerializeField] GameObject fon;

    public GameObject carPopUp;
    [SerializeField] InputField nameCar;
    [SerializeField] InputField vin;
    [SerializeField] InputField number;
    public bool isCar;
    public GameObject legalPopUp;
    [SerializeField] InputField nameLegal;
    [SerializeField] InputField inn;
    [SerializeField] InputField kpp;
    public bool isLegal;

    public void ClickOpenClose(bool flag){
        nameUser.text = "";
        family.text = "";
        nameFather.text = "";
        login.text = "";
        password.text = "";
        errorInfo.text = "";
        fon.SetActive(flag);
        
        if(!flag) return;
        isCar = contUser.user.role == role.carrier;
        isLegal = contUser.user.role == role.logist;
        carPopUp.SetActive(isCar);
        legalPopUp.SetActive(isLegal);
        if(isCar){
            nameCar.text = "";
            vin.text = "";
            number.text = "";
        }
        if(isLegal){
            nameLegal.text = "";
            inn.text = "";
            kpp.text = "";
        }
    }

    public void ClickCreate(){
        User user = new User(
            contUser.users.Count, nameUser.text, family.text, nameFather.text, login.text, isCar ? role.driver : (isLegal ? role.carrier : role.driver),
            isCar ? new Car(nameCar.text, vin.text, number.text) : null,
            isLegal ? new Legal(nameLegal.text, inn.text, kpp.text) : null,
            contUser.user.idUser
        );
        contUser.users.Add(user);
        contAuth.dataUserAuths.Add(new DataUserAuth(user.login, password.text, user.idUser));
        nameUser.text = "";
        family.text = "";
        nameFather.text = "";
        login.text = "";
        password.text = "";
        if(isCar){
            nameCar.text = "";
            vin.text = "";
            number.text = "";
        }
        if(isLegal){
            nameLegal.text = "";
            inn.text = "";
            kpp.text = "";
        }
        errorInfo.text = "Пользователь создан";
    }
}
