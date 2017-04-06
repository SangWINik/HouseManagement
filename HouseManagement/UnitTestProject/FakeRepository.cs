using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HouseManagement.Models;

namespace UnitTestProject
{
    class FakeRepository: IHouseRepository
    {
        public List<House> Houses = new List<House>(); 

        public IEnumerable<House> GetAll()
        {
            return Houses;
        }

        public House Get(int id)
        {
            return Houses.First(x => x.Id == id);
        }

        public House Add(House item)
        {
            Houses.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            House toRemove = Get(id);
            Houses.Remove(toRemove);
        }

        public bool Update(House item)
        {
            House toUpdate = Get(item.Id);
            if (toUpdate == null)
                return false;
            toUpdate.Address = item.Address;
            toUpdate.Square = item.Square;
            toUpdate.Photo = item.Photo;
            return true;
        }
    }
}
