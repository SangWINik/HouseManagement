using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using HouseManagement.Controllers;
using HouseManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class ControllerUnitTest
    {
        [TestMethod]
        public void GetAllTestMethod()
        {
            var testHouses = getTestData();
            HousesController.Repository = new FakeRepository();
            FakeRepository rep = (FakeRepository)HousesController.Repository;
            HousesController controller = new HousesController();
            rep.Houses = testHouses;
            Assert.AreEqual(testHouses.Count, controller.GetAllHouses().ToList().Count);
        }

        [TestMethod]
        public void DeleteTestMethod()
        {
            var testHouses = getTestData();
            HousesController.Repository = new FakeRepository();
            FakeRepository rep = (FakeRepository)HousesController.Repository;
            HousesController controller = new HousesController();
            rep.Houses = testHouses;
            controller.DeleteHouse(3);
            Assert.IsNull(rep.Houses.Find(x=>x.Id==3));
        }

        [TestMethod]
        public void UpdateTestMethod()
        {
            var testHouses = getTestData();
            HousesController.Repository = new FakeRepository();
            FakeRepository rep = (FakeRepository)HousesController.Repository;
            HousesController controller = new HousesController();
            rep.Houses = testHouses;
            House modified = new House() { Id = 3, Address = "ModifiedAddress", Square = 15 };
            controller.PutHouse(3, modified);
            Assert.AreEqual(controller.GetAllHouses().ToList()[2].Address, modified.Address);
            Assert.AreEqual(controller.GetAllHouses().ToList()[2].Square, modified.Square);
            Assert.AreEqual(controller.GetAllHouses().ToList()[2].Photo, modified.Photo);
        }

        private List<House> getTestData()
        {
            List<House> houses = new List<House>();
            houses.Add(new House() { Id = 1, Address = "Address1", Square = 12 });
            houses.Add(new House() { Id = 2, Address = "Address2", Square = 41 });
            houses.Add(new House() { Id = 3, Address = "Address3", Square = 21 });
            houses.Add(new House() { Id = 4, Address = "Address4", Square = 18 });
            houses.Add(new House() { Id = 5, Address = "Address5", Square = 15 });
            return houses;
        }
    }
}
