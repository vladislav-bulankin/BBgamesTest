using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RockPaperScissors.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameTransactionsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OperationType = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTransactionsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameTransactionsDb_UserDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchHistoryDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Player1Id = table.Column<int>(type: "integer", nullable: false),
                    Player2Id = table.Column<int>(type: "integer", nullable: false),
                    WinnerId = table.Column<int>(type: "integer", nullable: true),
                    Bet = table.Column<decimal>(type: "numeric", nullable: false),
                    MovePlayer1 = table.Column<string>(type: "text", nullable: true),
                    MovePlayer2 = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchHistoryDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchHistoryDb_UserDb_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "UserDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchHistoryDb_UserDb_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "UserDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchHistoryDb_UserDb_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "UserDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameTransactionsDb_UserId",
                table: "GameTransactionsDb",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistoryDb_Player1Id",
                table: "MatchHistoryDb",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistoryDb_Player1Id_Player2Id_CreatedAt",
                table: "MatchHistoryDb",
                columns: new[] { "Player1Id", "Player2Id", "CreatedAt" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistoryDb_Player2Id",
                table: "MatchHistoryDb",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistoryDb_WinnerId",
                table: "MatchHistoryDb",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDb_UserName",
                table: "UserDb",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTransactionsDb");

            migrationBuilder.DropTable(
                name: "MatchHistoryDb");

            migrationBuilder.DropTable(
                name: "UserDb");
        }
    }
}
