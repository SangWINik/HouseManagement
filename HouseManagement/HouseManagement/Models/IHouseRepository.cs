using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseManagement.Models
{
    public interface IHouseRepository
    {
        IEnumerable<House> GetAll();
        House Get(int id);
        House Add(House item);
        void Remove(int id);
        bool Update(House item);
    }
}
