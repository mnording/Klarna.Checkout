﻿using System;
using System.IO;
using Klarna.Entities;
using Klarna.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Klarna.Checkout
{
    public class Checkout
    {
        private MerchantConfig config;
        public Checkout(MerchantConfig config)
        {
            this.config = config;
        }
        /// <summary>
        /// Will create the Checkout session towards klarna 
        /// </summary>
        public CheckoutOrder Create(CheckoutOrder order)
        {
            DigestCreator digest = new DigestCreator();
            var auth = digest.CreateDigest(config.merchantId, config.sharedSecret);

            var url = GetApiLocation(config.Server);
            var ob = MakeObjectToString(order);
            CheckoutRequest req = new CheckoutRequest();
            var response = req.CreateRequest(auth, url, "POST", ob);

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd(); // do something fun...
                var jsreader = new JsonTextReader(new StringReader(result));
                order = new JsonSerializer().Deserialize<CheckoutOrder>(jsreader);
            }
            return order;
        }

        public CheckoutOrder GetOrderFromString(string data)
        {
            var jsreader = new JsonTextReader(new StringReader(data));
            var order = new JsonSerializer().Deserialize<CheckoutOrder>(jsreader);
            return order;

        }
        public string MakeObjectToString(CheckoutOrder order)
        {
            JsonSerializer jsonWriter = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            JObject ob = JObject.FromObject(order, jsonWriter);
            return ob.ToString();
        }
        public CheckoutOrder Read(string orderid)
        {
            DigestCreator digest = new DigestCreator();
            var auth = digest.CreateDigest(config.merchantId, config.sharedSecret);
            var url = GetApiLocation(config.Server, "/" + orderid);
            CheckoutRequest req = new CheckoutRequest();
            var response = req.CreateRequest(auth, url, "GET");
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd(); // do something fun...
                var jsreader = new JsonTextReader(new StringReader(result));
                var details = new JsonSerializer().Deserialize<CheckoutOrder>(jsreader);
                return details;
            }
        }
        public void Update(CheckoutOrder order)
        {
            DigestCreator digest = new DigestCreator();
            var auth = digest.CreateDigest(config.merchantId, config.sharedSecret);

            var url = GetApiLocation(config.Server, "/" + order.OrderId);
            var ob = MakeObjectToString(order);
            CheckoutRequest req = new CheckoutRequest();
            req.CreateRequest(auth, url, "POST", ob);

        }

        private Uri GetApiLocation(Server server, string append = "")
        {
            if (server == Server.Live)
            {
                return new Uri("https://api.klarna.com" + "/checkout/v3/orders" + append);
            }
            return new Uri("https://api.playground.klarna.com" + "/checkout/v3/orders" + append);
        }
    }
}
