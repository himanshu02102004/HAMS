using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management.Migrations
{
    /// <inheritdoc />
    public partial class initail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors");

            migrationBuilder.RenameColumn(
                name: "availability_slot",
                table: "doctors",
                newName: "Doctor_Availabiity");

            migrationBuilder.CreateTable(
                name: "doctorSchedules",
                columns: table => new
                {
                    Schedule_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Id = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_doctorSchedules", x => x.Schedule_Id);
                    table.ForeignKey(
                        name: "FK_doctorSchedules_doctors_Doctor_Id",
                        column: x => x.Doctor_Id,
                        principalTable: "doctors",
                        principalColumn: "Doctor_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_doctorSchedules_Doctor_Id",
                table: "doctorSchedules",
                column: "Doctor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors",
                column: "Department_Id",
                principalTable: "departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors");

            migrationBuilder.DropTable(
                name: "doctorSchedules");

            migrationBuilder.RenameColumn(
                name: "Doctor_Availabiity",
                table: "doctors",
                newName: "availability_slot");

            migrationBuilder.AddForeignKey(
                name: "FK_doctors_departments_Department_Id",
                table: "doctors",
                column: "Department_Id",
                principalTable: "departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
