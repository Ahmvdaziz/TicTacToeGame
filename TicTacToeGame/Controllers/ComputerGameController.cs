using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TicTacToeGame.Data;
using TicTacToeGame.Models;

namespace TicTacToeGame.Controllers
{
    public class ComputerGameController : Controller
    {
        private readonly GameDbContext _context;

        // Constructor to inject the database context
        public ComputerGameController(GameDbContext context)
        {
            _context = context;
        }

        // Displays the game board, creating a new game if none exists or if the last game is over
        public async Task<IActionResult> Index()
        {
            // Retrieve the most recent game from the database
            var game = await _context.Games.OrderByDescending(g => g.Id).FirstOrDefaultAsync();

            // If no game exists or the last game is over, create a new game
            if (game == null || game.IsGameOver)
            {
                game = new Game
                {
                    CurrentPlayer = "X",  // Player starts first
                    Board = "---------",   // Empty board (9 dashes representing cells)
                    Winner = "",
                    IsGameOver = false
                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();  // Save the new game to the database
            }

            return View(game);  // Return the game view
        }

        // Handles player moves
        [HttpPost]
        public async Task<IActionResult> Play(int cellIndex)
        {
            // Get the latest game
            var game = await _context.Games.OrderByDescending(g => g.Id).FirstOrDefaultAsync();
            if (game == null || game.IsGameOver) return RedirectToAction("Index");

            char[] board = game.Board.ToCharArray();

            // Player's move (X)
            if (board[cellIndex] == '-')
            {
                board[cellIndex] = 'X';  // Place X in the chosen cell
                game.Board = new string(board);
                game.Winner = CheckWinner(board);  // Check if the player won

                // If player wins, end the game and show the winner
                if (!string.IsNullOrEmpty(game.Winner))
                {
                    game.IsGameOver = true;
                    _context.Games.Update(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Winner", new { winner = game.Winner });
                }

                // Computer's move (O)
                int computerMove = GetComputerMove(board);
                if (computerMove != -1)
                {
                    board[computerMove] = 'O';  // Place O in the selected cell
                    game.Board = new string(board);
                    game.Winner = CheckWinner(board);  // Check if the computer won

                    // If the computer wins, end the game and show the winner
                    if (!string.IsNullOrEmpty(game.Winner))
                    {
                        game.IsGameOver = true;
                        _context.Games.Update(game);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Winner", new { winner = game.Winner });
                    }
                }
            }

            // Save the updated game state
            _context.Games.Update(game);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");  // Refresh the game board
        }

        // Displays the winner page
        public IActionResult Winner(string winner)
        {
            ViewBag.Winner = winner;  // Pass the winner to the view
            return View();
        }

        // Determines the computer's next move
        private int GetComputerMove(char[] board)
        {
            // First, check if the computer can win in the next move
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == '-')
                {
                    board[i] = 'O';  // Try placing 'O'
                    if (CheckWinner(board) == "O") return i;  // If this move wins, return it
                    board[i] = '-';  // Undo the move
                }
            }

            // Second, check if the player is about to win and block them
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == '-')
                {
                    board[i] = 'X';  // Try placing 'X' (simulating the player's move)
                    if (CheckWinner(board) == "X") return i;  // If this move allows X to win, block it
                    board[i] = '-';  // Undo the move
                }
            }

            // Otherwise, choose the first available empty cell
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == '-')
                    return i;
            }

            return -1;  // No available moves (should not happen if the game logic is correct)
        }

        // Checks if there is a winner
        private string CheckWinner(char[] board)
        {
            int[,] winningCombinations = new int[,]
            {
                { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 },  // Rows
                { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 },  // Columns
                { 0, 4, 8 }, { 2, 4, 6 }               // Diagonals
            };

            // Check each winning combination
            for (int i = 0; i < 8; i++)
            {
                if (board[winningCombinations[i, 0]] != '-' &&
                    board[winningCombinations[i, 0]] == board[winningCombinations[i, 1]] &&
                    board[winningCombinations[i, 1]] == board[winningCombinations[i, 2]])
                {
                    return board[winningCombinations[i, 0]].ToString();  // Return the winner (X or O)
                }
            }

            // Check if the board is full (draw)
            if (!board.Contains('-'))
            {
                return "Draw";
            }

            return "";  // No winner yet
        }
    }
}
