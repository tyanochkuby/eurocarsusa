using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class MultipleStatusProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            var getFilteredCarsCountPath = Path.Combine(basePath, "GetFilteredCarsCount2.sql");
            if (File.Exists(getFilteredCarsCountPath))
            {
                var getFilteredCarsSql = File.ReadAllText(getFilteredCarsCountPath);
                migrationBuilder.Sql(getFilteredCarsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getFilteredCarsCountPath}' not found.");
            }

            var getFilteredCarsPath = Path.Combine(basePath, "GetFilteredCars1.sql");
            if (File.Exists(getFilteredCarsPath))
            {
                var getFilteredCarsSql = File.ReadAllText(getFilteredCarsPath);
                migrationBuilder.Sql(getFilteredCarsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getFilteredCarsPath}' not found.");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            var getFilteredCarsCountPath = Path.Combine(basePath, "GetFilteredCarsCount1.sql");
            if (File.Exists(getFilteredCarsCountPath))
            {
                var getFilteredCarsSql = File.ReadAllText(getFilteredCarsCountPath);
                migrationBuilder.Sql(getFilteredCarsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getFilteredCarsCountPath}' not found.");
            }

            var getFilteredCarsPath = Path.Combine(basePath, "GetFilteredCars.sql");
            if (File.Exists(getFilteredCarsPath))
            {
                var getFilteredCarsSql = File.ReadAllText(getFilteredCarsPath);
                migrationBuilder.Sql(getFilteredCarsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getFilteredCarsPath}' not found.");
            }
        }
    }
}
