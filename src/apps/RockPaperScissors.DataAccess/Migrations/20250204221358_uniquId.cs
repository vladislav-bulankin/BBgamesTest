using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RockPaperScissors.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class uniquId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactionsDb_UserDb_Player1Id",
                table: "GameTransactionsDb");

            migrationBuilder.RenameColumn(
                name: "Player1Id",
                table: "GameTransactionsDb",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactionsDb_Player1Id",
                table: "GameTransactionsDb",
                newName: "IX_GameTransactionsDb_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Player2Id",
                table: "MatchHistoryDb",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Player1Id",
                table: "MatchHistoryDb",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactionsDb_UserDb_UserId",
                table: "GameTransactionsDb",
                column: "UserId",
                principalTable: "UserDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameTransactionsDb_UserDb_UserId",
                table: "GameTransactionsDb");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "GameTransactionsDb",
                newName: "Player1Id");

            migrationBuilder.RenameIndex(
                name: "IX_GameTransactionsDb_UserId",
                table: "GameTransactionsDb",
                newName: "IX_GameTransactionsDb_Player1Id");

            migrationBuilder.AlterColumn<int>(
                name: "Player2Id",
                table: "MatchHistoryDb",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Player1Id",
                table: "MatchHistoryDb",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameTransactionsDb_UserDb_Player1Id",
                table: "GameTransactionsDb",
                column: "Player1Id",
                principalTable: "UserDb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
