using DataBaseLayer.Interfaces;
using System.Data.Entity;
using QuickErrandsWebApi.Models;

namespace QuickErrandsWebApi.Repository
{
    public class ErrandsDatabase : DbContext, IDbContext
    {
        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}