using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Repository.Migrations
{
    public partial class v2dfffff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AreaId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Token = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Token);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TopicId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicTypes_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalFields",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PossibleValues = table.Column<string>(nullable: true),
                    FieldType = table.Column<string>(nullable: true),
                    TopicTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalFields_TopicTypes_TopicTypeId",
                        column: x => x.TopicTypeId,
                        principalTable: "TopicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    ApplicantId = table.Column<Guid>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDescription = table.Column<string>(nullable: true),
                    TopicTypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_TopicTypes_TopicTypeId",
                        column: x => x.TopicTypeId,
                        principalTable: "TopicTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalFieldsData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AdditionalFieldId = table.Column<Guid>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    RequestId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalFieldsData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalFieldsData_AdditionalFields_AdditionalFieldId",
                        column: x => x.AdditionalFieldId,
                        principalTable: "AdditionalFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalFieldsData_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFields_TopicTypeId",
                table: "AdditionalFields",
                column: "TopicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFieldsData_AdditionalFieldId",
                table: "AdditionalFieldsData",
                column: "AdditionalFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalFieldsData_RequestId",
                table: "AdditionalFieldsData",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ApplicantId",
                table: "Requests",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TopicTypeId",
                table: "Requests",
                column: "TopicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId",
                table: "Sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AreaId",
                table: "Topics",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTypes_TopicId",
                table: "TopicTypes",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalFieldsData");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "AdditionalFields");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "TopicTypes");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Areas");
        }
    }
}
