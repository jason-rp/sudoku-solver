using Sudoku.API.Models.Dto;

namespace Sudoku.API.Repository
{
    public interface ISudokuRepository
    {
        Task<IEnumerable<SudokuDto>> GetSudokusAsync();

        Task<SudokuDto> CreateUpdateSudoku(SudokuDto sudokuDto);
    }
}
