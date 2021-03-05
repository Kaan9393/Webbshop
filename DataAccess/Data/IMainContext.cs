using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IMainContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<CartItem> CartItem { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken token);
        void AddRange([NotNull] IEnumerable<object> entities);
    }
}