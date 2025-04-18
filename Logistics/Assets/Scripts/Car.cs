using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Car : MonoBehaviour
{
    public string nameCar;
    public string vin;
    public string number;

    public Car(string nameCar, string vin, string number){
        this.nameCar = nameCar;
        this.vin = vin;
        this.number = number;
    }
}
