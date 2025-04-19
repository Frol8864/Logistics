using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
//using System;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ContAuth : MonoBehaviour
{
    [SerializeField] Text loginField;
    [SerializeField] InputField passwordField;
    [SerializeField] Text errorInfo;
    [SerializeField] GameObject popUp;
    [SerializeField] ContUser contUser;
    private List<DataUserAuth> dataUserAuths;

    public void StartAuth(){
        popUp.SetActive(true);
        dataUserAuths = new List<DataUserAuth>(){
            new DataUserAuth("79675006885", "driver", 0),
            new DataUserAuth("79675006886", "logist", 1),
            new DataUserAuth("79675006887", "carrier", 2),
        };
    }

    public void ClickEnter(){
        errorInfo.GetComponent<Text>().text = "";
        string login = loginField.GetComponent<Text>().text;
        string password = passwordField.text;
        passwordField.text = "";
        for(int i = 0; i < dataUserAuths.Count; i++) {
            DataAnswerAuth dataAnswerAuth = dataUserAuths[i].ChechLogPass(login,password);
            if(dataAnswerAuth.succes) {
                popUp.SetActive(false);
                contUser.AuthUser(dataAnswerAuth.idUser);
                return;
            }
        }
        errorInfo.GetComponent<Text>().text = "Не подходит логин или пароль";
    }

    public void LogOut(){
        popUp.SetActive(true);
        errorInfo.GetComponent<Text>().text = "";
        passwordField.text = "";
    }
}

public class DataUserAuth {
    private string login;
    private string password;
    private int idUser;

    public DataAnswerAuth ChechLogPass(string _login, string _password){
        bool succes = (login == _login) && (password == _password);
        string error = succes ? "" : "Не подходит логин или пароль";
        DataAnswerAuth dataAnswerAuth = new DataAnswerAuth(succes, idUser, error);
        return dataAnswerAuth;
    }

    public DataUserAuth(string _login, string _password, int _idUser){
        login = _login;
        password = _password;
        idUser = _idUser;
    }
}

public class DataAnswerAuth {
    public bool succes;
    public int idUser;
    public string error;
    public DataAnswerAuth(bool _succes, int _idUser, string _error){
        succes = _succes;
        idUser = _idUser;
        error = _error;
    }
}