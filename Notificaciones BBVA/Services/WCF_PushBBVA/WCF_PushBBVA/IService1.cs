﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Data_PushBBVA;

namespace WCF_PushBBVA
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "Login/{user},{pass}")]
        bool DoWork(string user, string pass);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "Login2/{user},{pass}")]
        Login DoWork2(string user, string pass);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "NotificationType")]
        List<NotificationType> DoWork3();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "CreateUser/{user},{idDevice}")]
        String DoWork4(string user, string idDevice);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "Notification/{rut},{type}")]
        List<Notification> DoWork5(string rut, string type);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "NotificationAll/{type}")]
        List<Notification> DoWork6(string type);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "Token/{idDevice},{token}")]
        String DoWork7(string idDevice, string token);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ListSetting/{rut}")]
        List<NotificationUser> DoWork8(string rut);


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UserSetting")]
        Status<string> DoWork9();

        //[OperationContract]
        //[WebInvoke(Method = "GET",
        //    ResponseFormat = WebMessageFormat.Json,
        //    BodyStyle = WebMessageBodyStyle.WrappedRequest,
        //    UriTemplate = "Prueba")]
        //String DoWork1();

    }
}
