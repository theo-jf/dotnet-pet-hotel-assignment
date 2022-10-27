using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_bakery.Migrations
{
    public partial class tableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "petName",
                table: "Pets",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "checkedInTime",
                table: "Pets",
                newName: "checkedInAt");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "PetOwners",
                newName: "emailAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Pets",
                newName: "petName");

            migrationBuilder.RenameColumn(
                name: "checkedInAt",
                table: "Pets",
                newName: "checkedInTime");

            migrationBuilder.RenameColumn(
                name: "emailAddress",
                table: "PetOwners",
                newName: "email");
        }
    }
}
