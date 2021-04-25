using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Database.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository 
    {
        private readonly Context context;

        public AddressRepository(Context context) :base(context)
        {
            this.context = context;
        }


    }
}
