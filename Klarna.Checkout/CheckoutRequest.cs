using System;
using System.Net;
using System.Reflection;
using Klarna.Helpers;

namespace Klarna.Checkout
{
    class CheckoutRequest
    {
        private JsonRequest req;

        public CheckoutRequest()
        {
            req = new JsonRequest();
        }

        public HttpWebResponse CreateRequest(string digest, Uri url, string method, string body = null)
        {
            return req.CreateRequest(digest, url, method, body, "Mnording Checkout SDK - " + Assembly.GetExecutingAssembly().GetName().Version);
        }
    }
}
