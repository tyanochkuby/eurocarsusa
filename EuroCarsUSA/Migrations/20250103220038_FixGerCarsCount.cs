using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class FixGerCarsCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            // Execute GetFilteredCars.sql
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");
            var getFilteredCarsCountPath = Path.Combine(basePath, "GetFilteredCarsCount.sql");
            if (File.Exists(getFilteredCarsCountPath))
            {
                var getFilteredCarsCountSql = File.ReadAllText(getFilteredCarsCountPath);
                migrationBuilder.Sql(getFilteredCarsCountSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getFilteredCarsCountPath}' not found.");
            }
        }
    }
}
