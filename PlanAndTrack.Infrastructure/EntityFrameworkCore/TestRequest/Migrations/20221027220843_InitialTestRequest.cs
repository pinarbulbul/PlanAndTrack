using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PlanAndTrack.Infrastructure.EntityFrameworkCore.TestRequest.Migrations
{
    public partial class InitialTestRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PlannedPeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    ActualPeriodEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    AppliedType = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImportanceLevel = table.Column<int>(type: "integer", nullable: false),
                    TestType = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    File = table.Column<byte[]>(type: "bytea", nullable: true),
                    TimeRequired = table.Column<int>(type: "integer", nullable: true),
                    LeftTimeRequired = table.Column<int>(type: "integer", nullable: true),
                    PreRequestId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRequest_TestRequest_PreRequestId",
                        column: x => x.PreRequestId,
                        principalTable: "TestRequest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlanPerformance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlannedPeriodId = table.Column<int>(type: "integer", nullable: false),
                    TotalNumber = table.Column<int>(type: "integer", nullable: false),
                    PromisedNumber = table.Column<int>(type: "integer", nullable: false),
                    FinishedNumber = table.Column<int>(type: "integer", nullable: false),
                    TotalImportanceLevel = table.Column<int>(type: "integer", nullable: false),
                    PromisedImportanceLevel = table.Column<int>(type: "integer", nullable: false),
                    FinishedImportanceLevel = table.Column<int>(type: "integer", nullable: false),
                    PromisedTime = table.Column<int>(type: "integer", nullable: false),
                    FinishedTime = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPerformance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPerformance_PlanPeriod_PlannedPeriodId",
                        column: x => x.PlannedPeriodId,
                        principalTable: "PlanPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanPeriodResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlannedPeriodId = table.Column<int>(type: "integer", nullable: false),
                    TestType = table.Column<string>(type: "text", nullable: false),
                    Available = table.Column<int>(type: "integer", nullable: false),
                    Promised = table.Column<int>(type: "integer", nullable: false),
                    Used = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanPeriodResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanPeriodResource_PlanPeriod_PlannedPeriodId",
                        column: x => x.PlannedPeriodId,
                        principalTable: "PlanPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanTestRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestRequestId = table.Column<int>(type: "integer", nullable: false),
                    PlannedPeriodId = table.Column<int>(type: "integer", nullable: false),
                    TimeRequired = table.Column<int>(type: "integer", nullable: false),
                    LeftTimeRequired = table.Column<int>(type: "integer", nullable: false),
                    Assignee = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTestRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanTestRequest_PlanPeriod_PlannedPeriodId",
                        column: x => x.PlannedPeriodId,
                        principalTable: "PlanPeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanTestRequest_TestRequest_TestRequestId",
                        column: x => x.TestRequestId,
                        principalTable: "TestRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanPerformance_PlannedPeriodId",
                table: "PlanPerformance",
                column: "PlannedPeriodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanPeriodResource_PlannedPeriodId",
                table: "PlanPeriodResource",
                column: "PlannedPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTestRequest_PlannedPeriodId",
                table: "PlanTestRequest",
                column: "PlannedPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTestRequest_TestRequestId",
                table: "PlanTestRequest",
                column: "TestRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRequest_PreRequestId",
                table: "TestRequest",
                column: "PreRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanPerformance");

            migrationBuilder.DropTable(
                name: "PlanPeriodResource");

            migrationBuilder.DropTable(
                name: "PlanTestRequest");

            migrationBuilder.DropTable(
                name: "PlanPeriod");

            migrationBuilder.DropTable(
                name: "TestRequest");
        }
    }
}
