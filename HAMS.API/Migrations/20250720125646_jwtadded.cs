using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management.Migrations
{
    /// <inheritdoc />
    public partial class jwtadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_Password",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "User_Name",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "User_Id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SmptPort",
                table: "emailSettings",
                newName: "SmtpPort");

            migrationBuilder.RenameColumn(
                name: "SmptHost",
                table: "emailSettings",
                newName: "SmtpHost");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Users",
                newName: "User_Password");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "User_Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "User_Id");

            migrationBuilder.RenameColumn(
                name: "SmtpPort",
                table: "emailSettings",
                newName: "SmptPort");

            migrationBuilder.RenameColumn(
                name: "SmtpHost",
                table: "emailSettings",
                newName: "SmptHost");
        }
    }
}
