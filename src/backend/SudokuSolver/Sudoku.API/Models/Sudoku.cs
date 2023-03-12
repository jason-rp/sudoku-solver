using System.ComponentModel.DataAnnotations;

namespace Sudoku.API.Models
{
    public class Sudoku
    {
        [Key]
        public int SudokuHistoryId { get; set; }

        [Required]
        public string Board { get; set; }

        public DateTime SolvedDateTime { get; set; }

    }
}
