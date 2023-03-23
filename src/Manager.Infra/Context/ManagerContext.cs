using System;
using System.Threading.Tasks;
using Manager.Domain.Entities;
using Manager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Context{
    public class ManagerContext: DbContext{
        public ManagerContext()
        {}

        public ManagerContext(DbContextOptions<ManagerContext> options) : base(options)
        {}

        //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //  {
        //      optionsBuilder.UseSqlServer(@"Server=DESKTOP-7JPLA67\SQLEXPRESS1;Database=Manager;Trusted_Connection=True;");
        //  }

       public virtual DbSet<User> Users { get; set; }

       protected override void OnModelCreating(ModelBuilder builder)
       {
        builder.ApplyConfiguration(new UserMap());
       }
    }
}