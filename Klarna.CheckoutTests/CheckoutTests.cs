using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Klarna.Checkout;
using Klarna.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Klarna.CheckoutTests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void MustBeAbleToCreateCheckout()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var orderlines = new List<OrderLine>
            {
                new OrderLine("test", 2, 1000, 2500)
            };
            MerchantUrls url = new MerchantUrls();
            url.Terms = new Uri("http://www.test.com");
            url.Push = new Uri("http://www.test.com");
            url.CheckoutUri = new Uri("http://www.test.com");
            url.Confirmation = new Uri("http://www.test.com");
            Address ad = new Address();
            CheckoutOrder order = new CheckoutOrder(orderlines, url);
            order.Locale = "sv-se";
            order.PurchaseCountry = "se";
            order.PurchaseCurrency = "SEK";
           
            order =  check.Create(order);
            Assert.AreEqual("checkout_incomplete",order.Status);
            Assert.IsNotNull(order.Snippet);
        }

        [TestMethod]
        public void MustBeAbleToUpdateOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var orderlines = new List<OrderLine>
            {
                new OrderLine("test", 2, 1000, 2500)
            };
            MerchantUrls url = new MerchantUrls();
            url.Terms = new Uri("http://www.test.com");
            url.Push = new Uri("http://www.test.com");
            url.CheckoutUri = new Uri("http://www.test.com");
            url.Confirmation = new Uri("http://www.test.com");
            CheckoutOrder order = new CheckoutOrder(orderlines, url);
            order.Locale = "sv-se";
            order.PurchaseCountry = "se";
            order.PurchaseCurrency = "SEK";
            
            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);

           orderlines.Add(new OrderLine("tess",2,2300,2500));
            order.SetNewOrderlines(orderlines);
            check.Update(order);
        }
        [TestMethod]
        public void MustBeAbleToReadOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var orderlines = new List<OrderLine>
            {
                new OrderLine("test", 2, 1000, 2500)
            };
            MerchantUrls url = new MerchantUrls();
            url.Terms = new Uri("http://www.test.com");
            url.Push = new Uri("http://www.test.com");
            url.CheckoutUri = new Uri("http://www.test.com");
            url.Confirmation = new Uri("http://www.test.com");
            Address ad = new Address();
            CheckoutOrder order = new CheckoutOrder(orderlines, url);
            order.Locale = "sv-se";
            order.PurchaseCountry = "se";
            order.PurchaseCurrency = "SEK";

            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);

            CheckoutOrder newOrder = check.Read(order.OrderId);
            Assert.AreEqual(order.OrderId, newOrder.OrderId);
        }
    }
}
