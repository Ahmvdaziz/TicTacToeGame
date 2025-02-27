using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToeGame.Migrations
{
    /// <inheritdoc />
    public partial class new1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentTurn",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerO",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "PlayerX",
                table: "Games",
                newName: "CurrentPlayer");

            migrationBuilder.AddColumn<bool>(
                name: "IsGameOver",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGameOver",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "CurrentPlayer",
                table: "Games",
                newName: "PlayerX");

            migrationBuilder.AddColumn<string>(
                name: "CurrentTurn",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlayerO",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
