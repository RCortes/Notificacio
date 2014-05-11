using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Android;
using PushSharp.Core;
using System.Data.SqlClient;
using System.Data;
using System.Xml;


namespace Push_Notification
{
    class Program
    {
        static int enviado = 0;
        static int error = 0;
        static int i = 0;
        static XmlNodeList Notificaciones;

        static void Main(string[] args)
        {
            var push = new PushBroker();

            push.OnNotificationSent += NotificationSent;
            push.OnChannelException += ChannelException;
            push.OnServiceException += ServiceException;
            push.OnNotificationFailed += NotificationFailed;
            push.OnDeviceSubscriptionExpired += DeviceSubscriptionExpired;
            push.OnDeviceSubscriptionChanged += DeviceSubscriptionChanged;
            push.OnChannelCreated += ChannelCreated;
            push.OnChannelDestroyed += ChannelDestroyed;


         //   XmlDocument data = new XmlDocument();
            //data.Load("http://localhost:49167/Service1.svc/NotificationAll/0");
            //Notificaciones = data.GetElementsByTagName("Notification");
            //int total = Notificaciones.Count;

            var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Certificados.p12"));
            push.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, "q1w2e3r4"));
            push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBbsQnPByBI484hHMLOC_FRLowkIKqlWO0"));

            //for(i = 0; i < total-1; i++)
            //{


                //if (Notificaciones[i].ChildNodes[4].InnerText == "iOS")
                //{
                //    ////  APPLE

            for (int i = 0; i <= 30; i++)
            {
                push.QueueNotification(new AppleNotification()
                                           .ForDeviceToken("3290a71fec3cbb5baaf13dda7b465b82d7f4c552e9a8f69daf9f2679afb6b74d")
                                           .WithAlert("Hola Rodolfo Como estas? _" + i)
                                           .WithBadge(-1)
                                           .WithSound("sound.caf"));

            }

  //          //    }else if(Notificaciones[i].ChildNodes[4].InnerText == "Android"){


  //          for (int i = 0; i < 15; i++)
  //          {
  //              push.QueueNotification(new GcmNotification().ForDeviceRegistrationId("APA91bF17D0nVf-bp0Le3f5mqmxfZFRYs3Pxmfn9yib0LCVCvSjgUL3sYut814rrdSmQ0xq_w_tU2livvAfIH0pNafBY6WAG-NEdKiwc1vCtFT46v4Cqw5RVXFFaoNjXonbo4uPpvNJGqEvoEq9N3gWEqNn7d2Ya")
  //                                    .WithJson("{\"alert\":\"Hello World!\",\"badge\":7,\"sound\":\"sound.caf\"}"));
  //              //}
  //          }
  ////          }
            Console.WriteLine("Waiting for Queue to Finish...");
            push.StopAllServices();
            Console.WriteLine("enviados: " + enviado + " perdidos: " + error);
            Console.WriteLine("Queue Finished, press return to exit...");

            Console.ReadLine();	
        }

        static void DeviceSubscriptionChanged(object sender, string oldSubscriptionId, string newSubscriptionId, INotification notification)
		{
			//Currently this event will only ever happen for Android GCM
			Console.WriteLine("Device Registration Changed:  Old-> " + oldSubscriptionId + "  New-> " + newSubscriptionId + " -> " + notification);
		}

		static void NotificationSent(object sender, INotification notification)
		{
			Console.WriteLine("Sent: " + sender + " -> " + notification);
            enviado = enviado + 1;
          //  Console.WriteLine("Sent: " +Notificaciones[i].ChildNodes[9].InnerText);
           // System.Threading.Thread.Sleep(1000);
        }

		static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
		{
			Console.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
            error = error + 1;
           // Console.WriteLine("Error:" + Notificaciones[i].ChildNodes[9].InnerText);
           // System.Threading.Thread.Sleep(1000);
		}

		static void ChannelException(object sender, IPushChannel channel, Exception exception)
		{
			Console.WriteLine("Channel Exception: " + sender + " -> " + exception);
		}

		static void ServiceException(object sender, Exception exception)
		{
			Console.WriteLine("Channel Exception: " + sender + " -> " + exception);
		}

		static void DeviceSubscriptionExpired(object sender, string expiredDeviceSubscriptionId, DateTime timestamp, INotification notification)
		{
			Console.WriteLine("Device Subscription Expired: " + sender + " -> " + expiredDeviceSubscriptionId);
		}

		static void ChannelDestroyed(object sender)
		{
			Console.WriteLine("Channel Destroyed for: " + sender);
		}

		static void ChannelCreated(object sender, IPushChannel pushChannel)
		{
			Console.WriteLine("Channel Created for: " + sender);
		}
        
    }
}
