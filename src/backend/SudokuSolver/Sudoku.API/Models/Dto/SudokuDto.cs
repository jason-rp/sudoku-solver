using System.ComponentModel.DataAnnotations;

namespace Sudoku.API.Models.Dto
{
    public class SudokuDto
    {
        public int SudokuHistoryId { get; set; }

        public string Board { get; set; }

        public DateTime SolvedDateTime { get; set; }

        public string SolvedDate { get; set; }
    }
}
