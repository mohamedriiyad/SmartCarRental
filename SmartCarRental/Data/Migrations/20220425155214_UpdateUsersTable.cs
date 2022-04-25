using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRental.Data.Migrations
{
    public partial class UpdateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_UserId",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRent_AspNetUsers_RenterId",
                table: "CarRent");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRent_Car_CarId",
                table: "CarRent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRent_AspNetUsers_UserId",
                table: "UserRent");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRent_Car_CarId",
                table: "UserRent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRent",
                table: "UserRent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRent",
                table: "CarRent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "UserRent",
                newName: "UserRents");

            migrationBuilder.RenameTable(
                name: "CarRent",
                newName: "CarRents");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_UserRent_UserId",
                table: "UserRents",
                newName: "IX_UserRents_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRent_RenterId",
                table: "CarRents",
                newName: "IX_CarRents_RenterId");

            migrationBuilder.RenameIndex(
                name: "IX_Car_UserId",
                table: "Cars",
                newName: "IX_Cars_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRents",
                table: "UserRents",
                columns: new[] { "CarId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRents",
                table: "CarRents",
                columns: new[] { "CarId", "RenterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRents_AspNetUsers_RenterId",
                table: "CarRents",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRents_Cars_CarId",
                table: "CarRents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_UserId",
                table: "Cars",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRents_AspNetUsers_UserId",
                table: "UserRents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRents_Cars_CarId",
                table: "UserRents",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRents_AspNetUsers_RenterId",
                table: "CarRents");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRents_Cars_CarId",
                table: "CarRents");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_UserId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRents_AspNetUsers_UserId",
                table: "UserRents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRents_Cars_CarId",
                table: "UserRents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRents",
                table: "UserRents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRents",
                table: "CarRents");

            migrationBuilder.RenameTable(
                name: "UserRents",
                newName: "UserRent");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameTable(
                name: "CarRents",
                newName: "CarRent");

            migrationBuilder.RenameIndex(
                name: "IX_UserRents_UserId",
                table: "UserRent",
                newName: "IX_UserRent_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_UserId",
                table: "Car",
                newName: "IX_Car_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRents_RenterId",
                table: "CarRent",
                newName: "IX_CarRent_RenterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRent",
                table: "UserRent",
                columns: new[] { "CarId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRent",
                table: "CarRent",
                columns: new[] { "CarId", "RenterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_UserId",
                table: "Car",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRent_AspNetUsers_RenterId",
                table: "CarRent",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRent_Car_CarId",
                table: "CarRent",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRent_AspNetUsers_UserId",
                table: "UserRent",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRent_Car_CarId",
                table: "UserRent",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
