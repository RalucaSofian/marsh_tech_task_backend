using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace marsh_tech_task_backend.Migrations
{
    /// <inheritdoc />
    public partial class FullTextSearchSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF NOT EXISTS (
                    SELECT * FROM sys.fulltext_catalogs
                    WHERE [name] = N'DevicesFullTextCatalog'
                )
                BEGIN
                    CREATE FULLTEXT CATALOG DevicesFullTextCatalog
                END

                CREATE FULLTEXT INDEX ON Devices
                (
                    Name LANGUAGE 1033,
                    Manufacturer LANGUAGE 1033,
                    Processor LANGUAGE 1033
                ) KEY INDEX PK_Devices ON DevicesFullTextCatalog
                WITH CHANGE_TRACKING AUTO;
            ", suppressTransaction: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
