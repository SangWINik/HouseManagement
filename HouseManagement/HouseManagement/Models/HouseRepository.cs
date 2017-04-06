using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HouseManagement.DbContext;

namespace HouseManagement.Models
{
    public class HouseRepository: IHouseRepository
    {
        private HousesContext ctx;

        public HouseRepository()
        {
            ctx = new HousesContext();
        }

        public IEnumerable<House> GetAll()
        {
            List<House> h = ctx.Houses.ToList();
            return ctx.Houses;
        }

        public House Get(int id)
        {
            return ctx.Houses.Find(id);
        }

        public House Add(House item)
        {
            ctx.Houses.Add(item);
            ctx.SaveChanges();
            return item;
        }

        public void Remove(int id)
        {
            var houseToDelete = ctx.Houses.Find(id);
            if (houseToDelete != null)
            {
                ctx.Houses.Remove(houseToDelete);
                ctx.SaveChanges();
            }    
        }

        public bool Update(House item)
        {
            if (item == null)
                throw new ArgumentNullException();
            var houseToUpdate = ctx.Houses.Find(item.Id);
            if (houseToUpdate == null)
                return false;
            houseToUpdate.Address = item.Address;
            houseToUpdate.Photo = item.Photo;
            houseToUpdate.Square = item.Square;
            ctx.SaveChanges();
            return true;
        }
    }
}