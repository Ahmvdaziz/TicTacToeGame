using Microsoft.EntityFrameworkCore;
using TicTacToeGame.Models;

namespace TicTacToeGame.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }  // 
    }
}
