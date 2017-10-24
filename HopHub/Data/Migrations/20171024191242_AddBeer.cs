using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HopHub.Data.Migrations
{
    public partial class AddBeer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BeerID",
                table: "Entries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Beers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvgRating = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_BeerID",
                table: "Entries",
                column: "BeerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Beers_BeerID",
                table: "Entries",
                column: "BeerID",
                principalTable: "Beers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Beers_BeerID",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Entries_BeerID",
                table: "Entries");

            migrationBuilder.AlterColumn<string>(
                name: "BeerID",
                table: "Entries",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
