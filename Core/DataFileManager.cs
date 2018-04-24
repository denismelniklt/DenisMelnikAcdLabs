using Domain.Dto;
using Infrastructure;
using System.Collections.Generic;
using System.IO;

namespace Core
{
    public static class DataFileManager
    {
        public static IEnumerable<DataLineDto> GetLines(string filePath)
        {
            var lineDtos = new List<DataLineDto>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var lineDto = new DataLineDto();
                lineDto = line.GetDataLineDto();

                lineDtos.Add(lineDto);
            }

            return lineDtos;
        }
    }
}