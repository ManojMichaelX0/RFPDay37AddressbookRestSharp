using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookDay37;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestTestAddressBook
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private RestResponse getEmployeeList()
        {
            // Arrange
            // Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/addressBook", Method.Get);

            // Act
            // Execute the request
            RestResponse response = client.ExecuteAsync(request).Result;
            return response;
        }
        //UC 1
        [TestMethod]
        public void onCallingGETApi_ReturnAddressBookList()
        {
            RestResponse response = getEmployeeList();

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);     // Comes from using System.Net namespace
            List<AddressBook1> dataResponse = JsonConvert.DeserializeObject<List<AddressBook1>>(response.Content);
            Assert.AreEqual(3, dataResponse.Count);

            foreach (AddressBook1 a in dataResponse)
            {
                System.Console.WriteLine("FirstName : "+a.firstName+" LastName : "+a.lastName+" PhoneNumber : "+a.phoneNumber+" Address : "+a.address+" City : "+a.city+" State : "+a.state+" Zip : "+a.zip);
            }
        }

    }

}
