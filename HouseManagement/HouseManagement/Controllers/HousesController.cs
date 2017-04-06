using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HouseManagement.DbContext;
using HouseManagement.Models;

namespace HouseManagement.Controllers
{
    public class HousesController : ApiController
    {
        public static IHouseRepository Repository = new HouseRepository();

        /*public HousesController(IHouseRepository repository)
        {
            Repository = repository;
        }*/

        public IEnumerable<House> GetAllHouses()
        {
            return Repository.GetAll();
        }

        public HttpResponseMessage PostHouse(House item)
        {
            item = Repository.Add(item);
            var response = Request.CreateResponse(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutHouse(int id, House house)
        {
            house.Id = id;
            if (!Repository.Update(house))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public void DeleteHouse(int id)
        {
            House item = Repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Repository.Remove(id);
        }
    }
}
