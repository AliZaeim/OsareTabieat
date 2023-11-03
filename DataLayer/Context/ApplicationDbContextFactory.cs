using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer.Context
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer(@"Server=ZAEIMLENEVO\SQLSERVER2019;Database=Osare_db;Integrated Security=True;Trusted_Connection=True;encrypt=false");

            return new MyContext(optionsBuilder.Options);
        }        
    }
}
