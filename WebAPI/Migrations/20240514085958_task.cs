using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreAdvices",
                columns: table => new
                {
                    preAdviceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    depot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    liner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redelAuthNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    redelAuthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    vesselCarrier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vesselName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreAdvices", x => x.preAdviceId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreAdvices");
        }
    }
}
