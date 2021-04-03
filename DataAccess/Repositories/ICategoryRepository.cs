using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategorys();
        IEnumerable<Category> GetCategoriesBySearch(string search);
    }
}