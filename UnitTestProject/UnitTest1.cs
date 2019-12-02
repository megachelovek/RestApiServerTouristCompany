using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {

        private static string baseUrl = @"http://127.0.0.1:52743";
        [TestMethod]
        public void TestAuth()
        {
            string token = auth(baseUrl + "/token", "admin", "admin");
            Assert.IsTrue(token != null && token.Length != 0);
        }


        private string getAllEntity(string entityName)
        {
            string token = auth(baseUrl + "/token", "admin", "admin");
            string url = baseUrl + "/" + entityName;
            string result = null;
            List<dynamic> results = new List<dynamic>();

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Authorization", "Bearer " + token);
                result = webClient.DownloadString(url);
            }

            return result;
        }

        [TestMethod]
        public void TestGetAllClient()
        {
            string result = getAllEntity("GetAllClients");
            Assert.IsTrue(result != null && JArray.Parse(result).Count != 0);
        }

        [TestMethod]
        public void TestCreateNewTicket()
        {
            string token = auth(baseUrl + "/token", "admin", "admin");
            string url = baseUrl + "/api/Ticket";
            string lastId = (JArray.Parse(getAllEntity("api/Ticket")).Count + 100).ToString();

            string newUser =
@" {
        ""datetimeOfDepartureTicket"": ""2019 - 11 - 12T17: 46:04"",
        ""datetimeOfArrivalTicket"": ""2019-11-13T17:46:04"",
        ""id"": "+ lastId + @",
        ""placeOfDepartureTicket"": ""Test"",
        ""pointOfArrivalTicket"": ""Test"",
        ""dataTicket"": ""Test""
    }";

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Authorization", "Bearer " + token);
                webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json-patch+json");
                var r = webClient.UploadString(url, newUser);
            }

            var allTicket = JArray.Parse(getAllEntity("api/Ticket"));

            bool flag = false;

            foreach (var item in allTicket)
            {
                dynamic itemObj = JObject.Parse(item.ToString());
                string id = itemObj.id;
                if (id == lastId)
                {
                    flag = true;
                }
            }
            Assert.IsTrue(flag);


        }




        public string auth(string url, string login, string password)
        {
            using (var webClient = new WebClient())
            {
                var param = new NameValueCollection();
                param.Add("username", login);
                param.Add("password", password);
                webClient.QueryString = param;
                var resp = webClient.UploadString(url, "");
                dynamic respObject = JObject.Parse(resp);
                return respObject.access_token;
            }
        }
    }
}
