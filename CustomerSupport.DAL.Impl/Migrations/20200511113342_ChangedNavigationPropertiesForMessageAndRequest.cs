using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSupport.DAL.Impl.Migrations
{
    public partial class ChangedNavigationPropertiesForMessageAndRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Requests_RequestId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Message",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Requests_RequestId",
                table: "Message",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Requests_RequestId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests");

            migrationBuilder.AlterColumn<int>(
                name: "RequestId",
                table: "Message",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Requests_RequestId",
                table: "Message",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests",
                column: "SpecialistId",
                principalTable: "Specialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
