using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_departments_Department_Id1",
                table: "doctors");

            migrationBuilder.DropIndex(
                name: "IX_doctors_Department_Id1",
                table: "doctors");

            migrationBuilder.DropColumn(
                name: "Department_Id1",
                table: "doctors");

            migrationBuilder.CreateIndex(
                name: "IX_doctors_Department_Id",
                table: "doctors",
                column: "Department_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors",
                column: "Department_Id",
                principalTable: "departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors");

            migrationBuilder.DropIndex(
                name: "IX_doctors_Department_Id",
                table: "doctors");

            migrationBuilder.AddColumn<int>(
                name: "Department_Id1",
                table: "doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_doctors_Department_Id1",
                table: "doctors",
                column: "Department_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_departments_Department_Id1",
                table: "doctors",
                column: "Department_Id1",
                principalTable: "departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
