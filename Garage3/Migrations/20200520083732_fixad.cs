using Microsoft.EntityFrameworkCore.Migrations;

namespace Garage3.Migrations
{
    public partial class fixad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MemberId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "MembersId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypesId",
                table: "Vehicles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MembersId",
                table: "Vehicles",
                column: "MembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypesId",
                table: "Vehicles",
                column: "VehicleTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Members_MembersId",
                table: "Vehicles",
                column: "MembersId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypesId",
                table: "Vehicles",
                column: "VehicleTypesId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Members_MembersId",
                table: "Vehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypesId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MembersId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_VehicleTypesId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MembersId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "VehicleTypesId",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MemberId",
                table: "Vehicles",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Members_MemberId",
                table: "Vehicles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
