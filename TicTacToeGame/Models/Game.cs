using System.ComponentModel.DataAnnotations;

namespace TicTacToeGame.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string Board { get; set; } = "---------"; // 

        public string CurrentPlayer { get; set; } = "X"; // 

        public string Winner { get; set; } = "draw"; // 
        public bool IsGameOver { get; set; } = false; //   ؟
    }
}
