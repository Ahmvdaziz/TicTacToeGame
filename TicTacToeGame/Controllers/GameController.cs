using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Data;
using TicTacToeGame.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGame.Controllers
{
    public class GameController : Controller
    {
        private readonly GameDbContext _context;

        public GameController(GameDbContext context)
        {
            _context = context;
        }

        // Loads the latest game or starts a new one if no active game exists.
        public async Task<IActionResult> Index()
        {
            var game = await _context.Games.OrderByDescending(g => g.Id).FirstOrDefaultAsync();

            if (game == null || game.IsGameOver)
            {
                game = new Game
                {
                    CurrentPlayer = "X",
                    Board = "---------",
                    Winner = "",
                    IsGameOver = false
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();
            }

            return View(game);
        }

        // Handles a player's move.
        [HttpPost]
        public async Task<IActionResult> Play(int cellIndex)
        {
            var game = await _context.Games.OrderByDescending(g => g.Id).FirstOrDefaultAsync();
            if (game == null || game.IsGameOver) return RedirectToAction("Index");

            char[] board = game.Board.ToCharArray();

            // Ensure the selected cell is empty before making a move.
            if (board[cellIndex] == '-')
            {
                board[cellIndex] = game.CurrentPlayer[0];
                game.Board = new string(board);
                game.Winner = CheckWinner(board);

                if (!string.IsNullOrEmpty(game.Winner))
                {
                    game.IsGameOver = true;
                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Winner", new { winner = game.Winner });
                }
                else if (!game.Board.Contains('-'))
                {
                    game.IsGameOver = true;
                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Winner", new { winner = "Draw" });
                }
                else
                {
                    // Switch turn to the next player.
                    game.CurrentPlayer = (game.CurrentPlayer == "X") ? "O" : "X";
                }
            }

            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Displays the winner or redirects if there is none.
        public IActionResult Winner(string winner)
        {
            if (string.IsNullOrEmpty(winner))
            {
                return RedirectToAction("Index"); // If no winner, return to game
            }

            ViewBag.Winner = winner;
            return View();
        }

        // Checks if there is a winner based on winning combinations.
        private string CheckWinner(char[] board)
        {
            int[,] winningCombinations = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },
                { 0, 4, 8 }, { 2, 4, 6 }
            };

            for (int i = 0; i < 8; i++)
            {
                if (board[winningCombinations[i, 0]] != '-' &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 1]] &&
                    board[winningCombinations[i, 1]] == board[winningCombinations[i, 2]])
                {
                    return board[winningCombinations[i, 0]].ToString();
                }
            }

            return "";
        }

        // Resets the game by starting a new one.
        [HttpPost]
        public async Task<IActionResult> Reset()
        {
            var newGame = new Game
            {
                CurrentPlayer = "X",
                Board = "---------",
                Winner = "",
                IsGameOver = false
            };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
