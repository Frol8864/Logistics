using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Legal : MonoBehaviour
{
    public int idLegal;
    public string nameLegal;
    public string inn;
    public string kpp;

    public Legal(int idLegal, string nameLegal, string inn, string kpp){
        this.idLegal = idLegal;
        this.nameLegal = nameLegal;
        this.inn = inn;
        this.kpp = kpp;
    }
}
