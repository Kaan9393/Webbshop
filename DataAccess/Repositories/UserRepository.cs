using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMainContext _context;

        public UserRepository(IMainContext context)
        {
            _context = context;
        }

        public void CreateUser()
        {

        }
    }
}
