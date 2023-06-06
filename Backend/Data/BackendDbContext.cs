using Microsoft.EntityFrameworkCore;
using ExMoney.SharedLibs;

namespace ExMoney.Backend.Data
{
    public class BackendDbContext : DbContext
    {
        public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options) { }

        public DbSet<ExMoneySettings> ExMoneySettings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<KycVerification> KycVerifications { get; set; }
        public DbSet<PaymentProcessor> PaymentProcessors { get; set; }
        public DbSet<Wallet> ExMoneyWallets { get; set; }
        public DbSet<PaymentOperation> PaymentOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //apply configurations
            new CurrencyEntityConfiguration().Configure(modelBuilder.Entity<Currency>());

            new ExMoneySettingsEntityConfiguration().Configure(modelBuilder.Entity<ExMoneySettings>());

            new PaymentProcessorEntityConfiguration().Configure(modelBuilder.Entity<PaymentProcessor>());

            new WalletEntityConfiguration().Configure(modelBuilder.Entity<Wallet>());
        }
    }
}
