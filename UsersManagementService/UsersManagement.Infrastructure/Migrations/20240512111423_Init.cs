using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });


            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "PasswordHash" },
                values: new object[,]
                {
                     {
                        "4F95B908-997D-4DCF-99B4-29145ABED52B",
                        "admin",
                        "sCaw0ZphmmKSTvU8XfUBjeSuQ+PktjhhuAq/XNU5mJyXUTqFhehi57ZFtPDKQgx+AeMg7pZJxy/MZOtEDVkQ7Q==827958605CA5C5C80DD422DB28DE86FEDC7E828C85F036E3713316C95F8F54E496E2BC39D6380DAA3472DA2ACF405EF1D87367721FFF7DB30DBCC8E3965EABFD"
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
