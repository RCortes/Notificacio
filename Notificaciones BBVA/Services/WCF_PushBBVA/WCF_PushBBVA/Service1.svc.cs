﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Connection_PushBBVA;
using Data_PushBBVA;

namespace WCF_PushBBVA
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        //SQLConnection conx = new SQLConnection();

        public bool DoWork(String user, String pass)
        {
            bool resultado = SQLConnection.Login(user, pass);
            return resultado;
        }

        public Login DoWork2(String user, String pass)
        {
            Login login = new Login();
            login = SQLConnection.Login2(user, pass);
            return login;
        }

        public List<NotificationType> DoWork3()
        {
            List<NotificationType> Type = SQLConnection.NotificationType();
            return Type;
        }

        public string DoWork4(String user, String idDevice) 
        {
            String Create = SQLConnection.createUser(user, idDevice);
            return Create;
        }

        public String QuitarCarac(String rut)
        {
            rut = rut.Replace("-", "");
            rut = rut.Replace(".", "");
            return rut;
        }
     
    }
}