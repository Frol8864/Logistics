using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;


public class User : MonoBehaviour
{
    public int idUser;
    public string nameUser;
    public string family;
    public string nameFather;
    public string login;
    public role role;
    public Car car;
    public Legal legal;
    public int idUserCreate;

    public User(int _idUser, string _nameUser, string _family, string _nameFather, string _login, role _role, Car car, Legal legal, int idUserCreate){
        idUser = _idUser;
        nameUser = _nameUser;
        family = _family;
        nameFather = _nameFather;
        login = _login;
        role = _role;
        this.car = car;
        this.legal = legal;
        this.idUserCreate = idUserCreate;
    }
}

public enum role{
    driver,
    logist,
    carrier,
}