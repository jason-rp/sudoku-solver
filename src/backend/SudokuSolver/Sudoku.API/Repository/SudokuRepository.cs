using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sudoku.API.DbContexts;
using Sudoku.API.Models.Dto;

namespace Sudoku.API.Repository
{
    public class SudokuRepository : ISudokuRepository
    {
        public readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public SudokuRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SudokuDto> CreateUpdateSudoku(SudokuDto sudokuDto)
        {
            var sudokuHistory = _mapper.Map<SudokuDto, Models.Sudoku>(sudokuDto);
            if (sudokuHistory.SudokuHistoryId > 0)
            {
                _context.Sudokus.Update(sudokuHistory);
            }
            else 
            {
                _context.Sudokus.Add(sudokuHistory);
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<Models.Sudoku, SudokuDto>(sudokuHistory);
        }

        public async Task<IEnumerable<SudokuDto>> GetSudokusAsync()
        {
            var sudokuHistories = await _context.Sudokus.ToListAsync();
            return _mapper.Map<List<SudokuDto>>(sudokuHistories);
        }
    }
}
