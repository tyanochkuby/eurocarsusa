using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class MoveCarsFilteringToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Path to the directory where the SQL files are located
            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            // Execute GetFilteredCars.sql
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

            // Execute GetFilteredCarsCount.sql
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetFilteredCars");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetFilteredCarsCount");
        }
    }
}
