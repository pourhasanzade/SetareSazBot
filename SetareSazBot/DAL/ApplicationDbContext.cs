using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.DAL
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<ButtonEntity> Buttons { get; set; }
        public DbSet<UserDataEntity> UserData { get; set; }
        public DbSet<ConfigEntity> Configs { get; set; }
        public DbSet<ExceptionLogEntity> ExceptionLogs { get; set; }
        public DbSet<UserInfoEntity> UserInfo { get; set; }
        public DbSet<BankEntity> Banks { get; set; }
        public DbSet<ProvinceEntity> Provinces { get; set; }
        public DbSet<CityEntity> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new ButtonEntityConfiguration());
            modelBuilder.Configurations.Add(new UserDataEntityConfiguration());
            modelBuilder.Configurations.Add(new ConfigEntityConfiguration());
            modelBuilder.Configurations.Add(new ExceptionLogEntityConfiguration());
            modelBuilder.Configurations.Add(new UserInfoEntityConfiguration());
            modelBuilder.Configurations.Add(new BankEntityConfiguration());
            modelBuilder.Configurations.Add(new ProvinceEntityConfiguration());
            modelBuilder.Configurations.Add(new CityEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}