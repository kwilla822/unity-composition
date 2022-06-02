using System.Data.Entity.Migrations;

using FiveWattGroup.DataAccess.CodeFirst.Connections.Configuration;
using FiveWattGroup.DataAccess.CodeFirst.Migrations.Configuration.Environments;
using FiveWattGroup.DataAccess.Data;
using FiveWattGroup.Framework.Configuration;
using FiveWattGroup.Framework.Environments;

namespace FiveWattGroup.DataAccess.CodeFirst.Migrations.Configuration
{
    internal class ConfigurationMigration : DbMigrationsConfiguration<DataContext<ConfigurationConnection>>
    {
        public ConfigurationMigration()
        {
            AutomaticMigrationsEnabled = Settings.AutomaticMigrationsEnabled;
            AutomaticMigrationDataLossAllowed = Settings.AutomaticMigrationDataLossAllowed;
            MigrationsDirectory = @"CodeFirst\Migrations\Configuration";
        }

        protected override void Seed(DataContext<ConfigurationConnection> context)
        {
            switch (Settings.Environment)
            {
                case EnvironmentName.Local:
                    new Local().Seed(context);
                    break;

                case EnvironmentName.QA:
                    new Qa().Seed(context);
                    break;

                case EnvironmentName.UA:
                    new Ua().Seed(context);
                    break;
                
                case EnvironmentName.Production:
                    new Production().Seed(context);
                    break;
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}