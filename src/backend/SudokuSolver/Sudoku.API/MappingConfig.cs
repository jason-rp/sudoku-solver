using AutoMapper;
using Sudoku.API.Models.Dto;

namespace Sudoku.API
{
    public class MappingConfig 
    { 
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<SudokuDto, Models.Sudoku>().ReverseMap();
            });

            return mappingConfig; 
        }
    }
    
}
