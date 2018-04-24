using Domain;
using System.Collections.Generic;

namespace DAL
{
    public interface IDataLineManager
    {
        IList<DataLine> GetDataLines(string filePath);
    }
}