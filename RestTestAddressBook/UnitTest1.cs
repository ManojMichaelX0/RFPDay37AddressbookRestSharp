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

        //UC 23 add multiple address books

        //public void GivenMultipleEmployee_OnPost_ThenShouldReturnAdressBookList()
        //{
        //    // Arrange
        //    List<AddressBook1> addressBook = new List<AddressBook1>();
        //    addressBook.Add(new AddressBook1 { firstName = "Valorant",lastName = "xo",phoneNumber="9696870924",address="24-5-223",city="riot",state="Valorant",zip="40556",email="valorant@gmail,com" });
        //    addressBook.Add(new AddressBook1 { firstName = "Spartan", lastName = "Kratos", phoneNumber = "9696870454", address = "14-3-222", city = "god", state = "GodOfWar", zip = "40536", email = "godofwar@gmail,com" });

        //    // Iterate the loop for each 
        //    foreach (var a in addressBook)
        //    {
        //        // Initialize the request for POST to add new contact
        //        RestRequest request = new RestRequest("/addressBook", Method.Post);
        //        request.RequestFormat = DataFormat.Json;

        //        //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
        //        request.AddBody(a);

        //        //Act
        //        RestResponse response = client.ExecuteAsync(request).Result;

        //        //Assert
        //        Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        //        AddressBook1 address = JsonConvert.DeserializeObject<AddressBook1>(response.Content);
        //        Assert.AreEqual(a.firstName,address.firstName);
        //        Assert.AreEqual(a.lastName,address.lastName);
        //        Assert.AreEqual(a.phoneNumber,address.phoneNumber);
        //        Assert.AreEqual(a.address,address.address);
        //        Assert.AreEqual(a.city,address.city);
        //        Assert.AreEqual(a.state,address.state);
        //        Assert.AreEqual(a.zip,address.zip);
        //        Assert.AreEqual(a.email,address.email);
        //        Console.WriteLine(response.Content); 
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
    }
}
