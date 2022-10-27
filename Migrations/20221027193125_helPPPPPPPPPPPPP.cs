using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnet_bakery.Migrations
{
    public partial class helPPPPPPPPPPPPP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_ownedByid",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "ownedByid",
                table: "Pets",
                newName: "petOwnerid");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_ownedByid",
                table: "Pets",
                newName: "IX_Pets_petOwnerid");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_petOwnerid",
                table: "Pets",
                column: "petOwnerid",
                principalTable: "PetOwners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_PetOwners_petOwnerid",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "petOwnerid",
                table: "Pets",
                newName: "ownedByid");

            migrationBuilder.RenameIndex(
                name: "IX_Pets_petOwnerid",
                table: "Pets",
                newName: "IX_Pets_ownedByid");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_PetOwners_ownedByid",
                table: "Pets",
                column: "ownedByid",
                principalTable: "PetOwners",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
