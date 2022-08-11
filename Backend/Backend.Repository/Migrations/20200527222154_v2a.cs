using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Repository.Migrations
{
    public partial class v2a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Applicants_ApplicantId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ApplicantId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ApplicantId",
                table: "Requests");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Applicants",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_RequestId",
                table: "Applicants",
                column: "RequestId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Requests_RequestId",
                table: "Applicants",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Requests_RequestId",
                table: "Applicants");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_RequestId",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Applicants");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicantId",
                table: "Requests",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicantId",
                table: "Requests",
                column: "ApplicantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Applicants_ApplicantId",
                table: "Requests",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
