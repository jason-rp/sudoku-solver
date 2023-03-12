﻿namespace Sudoku.API.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;

        public object Result { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
