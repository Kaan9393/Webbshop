using DataAccess.Data;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMainContext _mainContext;

        public CategoryRepository(IMainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public IEnumerable<Category> GetAllCategorys()
        {
            return _mainContext.Categories.AsEnumerable();
        }
    }
}
