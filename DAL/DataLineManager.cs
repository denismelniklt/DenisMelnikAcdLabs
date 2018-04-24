using AutoMapper;
using Core;
using Domain;
using System.Collections.Generic;

namespace DAL
{
    public class DataLineManager : IDataLineManager
    {
        public IList<DataLine> GetDataLines(string filePath)
        {
            var dataLineDtos = DataFileManager.GetLines(filePath);
            var dataLines = new List<DataLine>();

            foreach (var dataLineDto in dataLineDtos)
            {
                DataLine dataLine;

                dataLine = Mapper.Map<DataLine>(dataLineDto);

                dataLines.Add(dataLine);
            }

            return dataLines;
        }
    }
}