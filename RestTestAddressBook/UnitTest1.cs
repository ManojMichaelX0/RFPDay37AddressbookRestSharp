using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBookDay37;
using RestSharp;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

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
        //UC 22
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
                System.Console.WriteLine("FirstName : " + a.firstName + " LastName : " + a.lastName + " PhoneNumber : " + a.phoneNumber + " Address : " + a.address + " City : " + a.city + " State : " + a.state + " Zip : " + a.zip + " email : " + a.email);
            }
        }

        //UC 23 -add mmultiple contacts
        [TestMethod]
        public void OnCallingPostAPIForAContactListWithMultipleContacts_ReturnContactObject()
        {
            // Arrange
            List<AddressBook1> contactList = new List<AddressBook1>();
            contactList.Add(new AddressBook1 { firstName = "Aron", lastName = "Stone", phoneNumber = "1234567890", address = "Dholakpur", city = "Varanasi", state = "UP", zip = "229554", email = "ps@gmail.com" });
            contactList.Add(new AddressBook1 { firstName = "Vishal", lastName = "Seth", phoneNumber = "781654987", address = "Charashu Chauraha", city = "Jaunpur", state = "UP", zip = "442206", email = "vs@gmail.com" });
            contactList.Add(new AddressBook1 { firstName = "Ekta", lastName = "Kumbhare", phoneNumber = "7856239865", address = "Bajaj Nagar", city = "Pune", state = "Maharashtra", zip = "442203", email = "ek@gmail.com" });

            //Iterate the loop for each contact
            foreach (var v in contactList)
            {
                //Initialize the request for POST to add new contact
                RestRequest request = new RestRequest("/addressBook", Method.Post);
                request.RequestFormat = DataFormat.Json;

                //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
                request.AddBody(v);

                //Act
                RestResponse response = client.ExecuteAsync(request).Result;

                //Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                AddressBook1 contact = JsonConvert.DeserializeObject<AddressBook1>(response.Content);
                Assert.AreEqual(v.firstName, contact.firstName);
                Assert.AreEqual(v.lastName, contact.lastName);
                Assert.AreEqual(v.phoneNumber, contact.phoneNumber);
                Console.WriteLine(response.Content);
            }
        }
        //UC 24 Update contact
        [TestMethod]
        public void OnCallingPutAPI_ReturnEmployeeObject()
        {
            // Arrange
            // Initialize the request for PUT to add new employee
            RestRequest request = new RestRequest("/addressBook/2", Method.Put);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new AddressBook1
            {
                firstName = "Manu",
                lastName ="Thiparapu",
                phoneNumber = "8978977310",
                address ="canada",
                city="torento",
                state ="torento",
                zip="406054",
                email ="manuthiparapu@gmail.com"
            });

            // Act
            RestResponse response = client.ExecuteAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            AddressBook1 address = JsonConvert.DeserializeObject<AddressBook1>(response.Content);
            Assert.AreEqual("Manu", address.firstName);
            Assert.AreEqual("8978977310", address.phoneNumber);
            Console.WriteLine(response.Content);
        }

        //UC 25 Delete Record
        [TestMethod]
        public void OnCallingDeleteAPI_ReturnSuccessStatus()
        {
            // Arrange
            // Initialize the request for PUT to add new 
            RestRequest request = new RestRequest("/addressBook/2", Method.Delete);

            // Act
            RestResponse response = client.ExecuteAsync(request).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}
