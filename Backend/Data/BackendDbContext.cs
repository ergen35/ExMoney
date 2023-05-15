using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;

namespace ExMoney.Backend.Data
{
    public class BackendDbContext: DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options): base(options){}

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
