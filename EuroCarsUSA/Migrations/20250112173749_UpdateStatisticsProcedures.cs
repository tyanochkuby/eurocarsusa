using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EuroCarsUSA.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatisticsProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE EventHistory (
                    Id UNIQUEIDENTIFIER PRIMARY KEY,
                    CarId UNIQUEIDENTIFIER,
                    EventDate DATETIME,
                    EventType NVARCHAR(10)
                );
            ");

            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            // Execute UpdateLikesCount.sql
            var updateLikesCountPath = Path.Combine(basePath, "UpdateLikesCount1.sql");
            if (File.Exists(updateLikesCountPath))
            {
                var updateLikesCountSql = File.ReadAllText(updateLikesCountPath);
                migrationBuilder.Sql(updateLikesCountSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{updateLikesCountPath}' not found.");
            }

            // Execute UpdateViewsCount.sql
            var updateViewsCountPath = Path.Combine(basePath, "UpdateViewsCount1.sql");
            if (File.Exists(updateViewsCountPath))
            {
                var updateViewsCountSql = File.ReadAllText(updateViewsCountPath);
                migrationBuilder.Sql(updateViewsCountSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{updateViewsCountPath}' not found.");
            }

            // Execute GetTotalStats.sql
            var getTotalStatsPath = Path.Combine(basePath, "GetTotalStats1.sql");
            if (File.Exists(getTotalStatsPath))
            {
                var getTotalStatsSql = File.ReadAllText(getTotalStatsPath);
                migrationBuilder.Sql(getTotalStatsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getTotalStatsPath}' not found.");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS EventHistory");

            var basePath = Path.Combine(AppContext.BaseDirectory, "Migrations", "Procedures");

            // Revert UpdateLikesCount.sql
            var updateLikesCountPath = Path.Combine(basePath, "UpdateLikesCount.sql");
            if (File.Exists(updateLikesCountPath))
            {
                var updateLikesCountSql = File.ReadAllText(updateLikesCountPath);
                migrationBuilder.Sql(updateLikesCountSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{updateLikesCountPath}' not found.");
            }

            // Revert UpdateViewsCount.sql
            var updateViewsCountPath = Path.Combine(basePath, "UpdateViewsCount.sql");
            if (File.Exists(updateViewsCountPath))
            {
                var updateViewsCountSql = File.ReadAllText(updateViewsCountPath);
                migrationBuilder.Sql(updateViewsCountSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{updateViewsCountPath}' not found.");
            }

            // Revert GetTotalStats.sql
            var getTotalStatsPath = Path.Combine(basePath, "GetTotalStats.sql");
            if (File.Exists(getTotalStatsPath))
            {
                var getTotalStatsSql = File.ReadAllText(getTotalStatsPath);
                migrationBuilder.Sql(getTotalStatsSql);
            }
            else
            {
                throw new FileNotFoundException($"SQL file '{getTotalStatsPath}' not found.");
            }
        }
    }
}
