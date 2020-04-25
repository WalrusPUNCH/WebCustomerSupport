using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerSupport.DAL.Impl.Migrations
{
    public partial class ChangedSpecialistOnDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests");

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
                name: "FK_Requests_Specialists_SpecialistId",
                table: "Requests");

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
