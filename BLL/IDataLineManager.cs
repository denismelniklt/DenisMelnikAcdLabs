using Domain;
using System.Collections.Generic;

namespace BLL
{
    public interface IDataLineManager
    {
        IList<DataLine> GetSortedDataLines(string inputFilePath);
    }
}