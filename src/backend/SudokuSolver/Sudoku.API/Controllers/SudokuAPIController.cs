using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sudoku.API.Models.Dto;
using Sudoku.API.Repository;

namespace Sudoku.API.Controllers
{
    [Route("api/sudokus")]
    [EnableCors("CorsPolicy")]
    public class SudokuAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private ISudokuRepository _sudokuRepository;

        public SudokuAPIController(ISudokuRepository sudokuRepository)
        {
            _sudokuRepository = sudokuRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get() 
        {
            try
            {
                var result = await _sudokuRepository.GetSudokusAsync();
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] SudokuDto sudokuDto) 
        {
            try
            {
                if (sudokuDto != null && !string.IsNullOrEmpty(sudokuDto.Board))
                {
                    sudokuDto.SolvedDateTime = DateTime.Now;
                }
                var result = await _sudokuRepository.CreateUpdateSudoku(sudokuDto);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] SudokuDto sudokuDto)
        {
            try
            {
                var result = await _sudokuRepository.CreateUpdateSudoku(sudokuDto);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message.ToString() };
            }
            return _response;
        }
    }
}
