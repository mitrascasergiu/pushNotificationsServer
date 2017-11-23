using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushNotificationsServer.Models
{
    public class SubscriptionKeysModel
    {
        [JsonProperty("auth")]
        public string Auth { get; set; }

        [JsonProperty("p256dh")]
        public string P256dh { get; set; }
    }
}