using System.Web.Http;
using System.IO;
using PushNotificationsServer.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using WebPush;
using System;

namespace PushNotificationsServer.Controllers
{
    public class SubscriptionController : ApiController
    {
        private string filePath = @"C:\Users\SMitrasca\Documents\visual studio 2017\Projects\PushNotificationsServer\PushNotificationsServer\Content\Subscription.txt";

        [HttpGet]
        public void Post(string endpoint, string p256dh, string auth)
        {
            var subscription = new SubscriptionModel
            {
                Endpoint = endpoint,
                Keys = new SubscriptionKeysModel
                {
                    Auth = auth,
                    P256dh = p256dh
                }
                
            };
            var sub = JsonConvert.SerializeObject(subscription);
            var subText = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(subText))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(sub);
                }
            }
            File.WriteAllText(filePath, sub);
        }

        [HttpGet]
        public void Send()
        {
            var subText = File.ReadAllText(filePath);
            var sub = JsonConvert.DeserializeObject<SubscriptionModel>(subText);

            VapidDetails vapidKeys = VapidHelper.GenerateVapidKeys();
            var subject = "mailto:simple-push-demo@gauntface.co.uk";

            var subscription = new PushSubscription(sub.Endpoint, sub.Keys.P256dh, sub.Keys.Auth);
            var vapidDetails = new VapidDetails(subject, vapidKeys.PublicKey, vapidKeys.PrivateKey);

            var webPushClient = new WebPushClient();
            try
            {
                webPushClient.SendNotification(subscription, "payload", vapidDetails);
            }
            catch (WebPushException exception)
            {
            }
            catch (Exception ex)
            {
            }
        }
    }
}
