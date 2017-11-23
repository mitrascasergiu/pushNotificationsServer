using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushNotificationsServer.Models
{
    public class SubscriptionModel
    {
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("keys")]
        public SubscriptionKeysModel Keys { get; set; }
    }
}