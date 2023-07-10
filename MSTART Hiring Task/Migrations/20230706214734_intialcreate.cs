using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSTART_Hiring_Task.Migrations
{
    /// <inheritdoc />
    public partial class intialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Server_DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTime_UTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    Update_DateTime_UTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    Date_Of_Birth = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC27847379C6", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Server_DateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateTime_UTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    Update_DateTime_UTC = table.Column<DateTime>(type: "datetime", nullable: false),
                    Account_Number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Balance = table.Column<decimal>(type: "money", nullable: false),
                    currency = table.Column<int>(type: "int", unicode: false, maxLength: 255, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__3214EC277C5010B3", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Account__User_ID__398D8EEE",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_User_ID",
                table: "Account",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId1",
                table: "Account",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
