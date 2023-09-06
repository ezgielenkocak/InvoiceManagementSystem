using InvoiceManagementSystem.Core.Entities.Concrete;
using InvoiceManagementSystem.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InvoiceManagementSystem.DAL.Concrete.Context
{
    public class ImsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=your-server-name;Database=your-db-name;Uid=sa;Password=your-db-password");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<UserApartment> UserApartments { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<UserBill> UserBills { get; set; }
        public DbSet<OperationClaim> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<BillType> BillTypes { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<UserSecurityHistory> UserSecurityHistories { get; set; }
        public DbSet<RoleGroupPermissions> RoleGroupsPermissions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<UserRoleGroups> UserRoleGroups { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
    }
}
