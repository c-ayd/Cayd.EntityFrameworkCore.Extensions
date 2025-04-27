#if NET8_0_OR_GREATER
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cayd.EntityFrameworkCore.Extensions.Test.Api.Migrations
{
    /// <inheritdoc />
    public partial class TestInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestComposites",
                columns: table => new
                {
                    Id1 = table.Column<int>(type: "integer", nullable: false),
                    Id2 = table.Column<Guid>(type: "uuid", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    StrValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestComposites", x => new { x.Id1, x.Id2 });
                });

            migrationBuilder.CreateTable(
                name: "TestParents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StrValue = table.Column<string>(type: "text", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    ValueObject_IntValue = table.Column<int>(type: "integer", nullable: false),
                    ValueObject_StrValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestParents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestChildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StrValue = table.Column<string>(type: "text", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    TestParentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestChildren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestChildren_TestParents_TestParentId",
                        column: x => x.TestParentId,
                        principalTable: "TestParents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestChildren_TestParentId",
                table: "TestChildren",
                column: "TestParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestChildren");

            migrationBuilder.DropTable(
                name: "TestComposites");

            migrationBuilder.DropTable(
                name: "TestParents");
        }
    }
}
#else
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cayd.EntityFrameworkCore.Extensions.Test.Api.Migrations
{
    public partial class TestInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestComposites",
                columns: table => new
                {
                    Id1 = table.Column<int>(type: "integer", nullable: false),
                    Id2 = table.Column<Guid>(type: "uuid", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    StrValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestComposites", x => new { x.Id1, x.Id2 });
                });

            migrationBuilder.CreateTable(
                name: "TestParents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StrValue = table.Column<string>(type: "text", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    ValueObject_StrValue = table.Column<string>(type: "text", nullable: false),
                    ValueObject_IntValue = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestParents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestChildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StrValue = table.Column<string>(type: "text", nullable: false),
                    IntValue = table.Column<int>(type: "integer", nullable: false),
                    TestParentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestChildren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestChildren_TestParents_TestParentId",
                        column: x => x.TestParentId,
                        principalTable: "TestParents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestChildren_TestParentId",
                table: "TestChildren",
                column: "TestParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestChildren");

            migrationBuilder.DropTable(
                name: "TestComposites");

            migrationBuilder.DropTable(
                name: "TestParents");
        }
    }
}
#endif
