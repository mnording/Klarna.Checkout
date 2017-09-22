using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Threading;
using Klarna.Checkout;
using Klarna.Checkout.Entities;
using Klarna.Entities;
using Klarna.Exception;
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
            var order = getBaseOrder();
            order =  check.Create(order);
            Assert.AreEqual("checkout_incomplete",order.Status);
            Assert.IsNotNull(order.Snippet);
        }

        [TestMethod]
        public void MustBeAbleToUpdateOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var order = getBaseOrder();

            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);
            var orderlines = new List<CheckoutOrderLine>
            {
                new CheckoutOrderLine("test", 2, 1000, 2500)
            };
            orderlines.Add(new CheckoutOrderLine("tess",2,2300,2500));
            order.SetNewOrderlines(orderlines);
            check.Update(order);
        }
        [TestMethod]
        public void MustBeAbleToReadOrder()
        {
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);

            var order = getBaseOrder();
            order = check.Create(order);
            Assert.AreEqual("checkout_incomplete", order.Status);
            Assert.IsNotNull(order.Snippet);

            CheckoutOrder newOrder = check.Read(order.OrderId);
            Assert.AreEqual(order.OrderId, newOrder.OrderId);
        }

        [TestMethod]
        public void MustBeAbleToReadCorrelationOfException()
        {
            var order = getBaseOrder();
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            order.OrderAmount = 00;
            try
            {
                check.Create(order);
            }
            catch (ApiException ex)
            {
                Assert.IsNotNull(ex.correlation_id);
            }
        }

        [TestMethod]
        public void MustBeAbleToCreateFromJson()
        {
            var StringRepresentationOfOrder = "{\"purchase_country\": \"gb\",\"purchase_currency\": \"gbp\",\"locale\": \"en-gb\",\"order_amount\": 10000,\"order_tax_amount\": 2000,\"order_lines\": [ {   \"type\": \"physical\",   \"reference\": \"123050\",   \"name\": \"Tomatoes\",   \"quantity\": 10,   \"quantity_unit\": \"kg\",   \"unit_price\": 600,   \"tax_rate\": 2500,   \"total_amount\": 6000,   \"total_tax_amount\": 1200 },  {   \"type\": \"physical\",   \"reference\": \"543670\",   \"name\": \"Bananas\",   \"quantity\": 1,   \"quantity_unit\": \"bag\",   \"unit_price\": 5000,   \"tax_rate\": 2500,   \"total_amount\": 4000,   \"total_discount_amount\": 1000,   \"total_tax_amount\": 800 }  ],\"merchant_urls\": { \"terms\": \"http://toc\", \"checkout\": \"http://checkout?klarna_order_id={checkout.order.id}\", \"confirmation\": \"http://confirmation?klarna_order_id={checkout.order.id}\", \"push\": \"http://push?klarna_order_id={checkout.order.id}\", \"validation\": \"http://validation?klarna_order_id={checkout.order.id}\"  }}";
            MerchantConfig config = new MerchantConfig("K500746", "fia'w2ahSheahahc", Server.Playground);
            Checkout.Checkout check = new Checkout.Checkout(config);
            var ok = check.GetOrderFromString(StringRepresentationOfOrder);
            Assert.AreEqual("en-gb",ok.Locale);
            Assert.AreEqual(2000, ok.OrderTaxAmount);
        }
        private CheckoutOrder getBaseOrder()
        {
            var orderlines = new List<CheckoutOrderLine>
            {
                new CheckoutOrderLine("test", 2, 1000, 2500)
            };
            MerchantUrls url = new MerchantUrls
            {
                Terms = new Uri("http://www.test.com"),
                Push = new Uri("http://www.test.com"),
                CheckoutUri = new Uri("http://www.test.com"),
                Confirmation = new Uri("http://www.test.com")
            };
            CheckoutOrder order = new CheckoutOrder(orderlines, url)
            {
                Locale = "sv-se",
                PurchaseCountry = "se",
                PurchaseCurrency = "SEK"
            };
            return order;
        }

    }
}
