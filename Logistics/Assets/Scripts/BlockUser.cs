using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class BlockUser : MonoBehaviour
{
    [SerializeField] Text info;
    [SerializeField] GameObject empty;

    [SerializeField] CreateRequestPopUp createRequestPopUp;

    private bool isUser;
    private User user;
    private role role;

    public void Clear(){
        empty.SetActive(true);
        isUser = false;
    }

    public void AddUser(User user, role role){
        empty.SetActive(false);
        isUser = true;
        this.user = user;
        this.role = role;

        if(role == role.driver) info.text = user.family + " " + user.nameUser + " " + user.nameFather + ", " + user.car.nameCar + " " + user.car.number + " " + user.car.vin;
        if(role == role.carrier) info.text = user.legal.nameLegal + " " + user.legal.inn + " " + user.legal.kpp;
    }

    private void OnMouseUp() {
        if(!isUser) return;
        createRequestPopUp.UpdateUser(user, role);
    }
}
