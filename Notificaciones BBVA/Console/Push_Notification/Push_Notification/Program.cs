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


namespace Push_Notification
{
    class Program
    {
       

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





            ////  APPLE
            var appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Certificados.p12"));
            push.RegisterAppleService(new ApplePushChannelSettings(false, appleCert, "q1w2e3r4")); 

            for (int i = 0; i < 10; i++)
            {
                push.QueueNotification(new AppleNotification()
                                           .ForDeviceToken("3290a71fec3cbb5baaf13dda7b465b82d7f4c552e9a8f69daf9f2679afb6b74d")
                                           .WithAlert("Hello, how are you? " + i)
                                           .WithBadge(-1)
                                           .WithSound("sound.caf"));

                
               
            }


            //// ANDROID

            for (int i = 0; i < 10; i++)
            {
                push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBbsQnPByBI484hHMLOC_FRLowkIKqlWO0"));
                push.QueueNotification(new GcmNotification().ForDeviceRegistrationId("APA91bHlrbYHS9T-fKoFHYXejLitdKjpQTE0W46p_UqOjpfQcFPPOiiAaEScmWdq_CfsOTgGRScuzOA7TNDYI7BYqgdE3_YkFJncsE3qIDeeuX75CWhco_h6iMnqA7_jO-W9ldxyqL6qxC2pZbE73QYSiS1X6htmuQ").ForDeviceRegistrationId("APA91bHlrbYHS9T-fKoFHYXejLitdKjpQTE0W46p_UqOjpfQcFPPOiiAaEScmWdq_CfsOTgGRScuzOA7TNDYI7BYqgdE3_YkFJncsE3qIDeeuX75CWhco_h6iMnqA7_jO-W9ldxyqL6qxC2pZbE73QYSiS1X6htmuQ")
                                .WithJson("{\"alert\":\"Hello CHE!\",\"badge\":7,\"sound\":\"sound.caf\"}"));


            }
            Console.WriteLine("Waiting for Queue to Finish...");
            push.StopAllServices();
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
		}

		static void NotificationFailed(object sender, INotification notification, Exception notificationFailureException)
		{
			Console.WriteLine("Failure: " + sender + " -> " + notificationFailureException.Message + " -> " + notification);
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
