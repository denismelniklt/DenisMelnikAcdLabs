using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

using IDataLineManagerDal = DAL.IDataLineManager;

namespace BLL
{
    public class DataLineManager : IDataLineManager
    {
        private readonly IDataLineManagerDal DataLineManagerDal;

        private int MaxFragmentCount = 0;

        public DataLineManager(IDataLineManagerDal dataLineManagerDal)
        {
            DataLineManagerDal = dataLineManagerDal;
        }

        public IList<DataLine> GetSortedDataLines(string filePath)
        {
            var dataLines = DataLineManagerDal.GetDataLines(filePath);
            var dataLineMatrix = new List<DataLine>();

            foreach (var dataLine in dataLines)
            {
                if (dataLine.Fragments.Count > MaxFragmentCount)
                {
                    MaxFragmentCount = dataLine.Fragments.Count;
                }
            }

            var dataLinesCount = dataLines.Count();

            // Creating the matrix with null values to comply with sorting algorithm.
            for (var i = 0; i < dataLinesCount; i++)
            {
                var currentDataLine = dataLines[i];
                var currentDataLineFragmentCount = currentDataLine.Fragments.Count();

                dataLineMatrix.Add(currentDataLine);

                for (var j = currentDataLineFragmentCount; j < MaxFragmentCount; j++)
                {
                    Fragment newDataLineFragment = new Fragment
                    {
                        Type = FragmentType.Empty,
                        Value = null
                    };
                    dataLineMatrix[i].Fragments.Add(newDataLineFragment);
                }
            }
            
            Sort(dataLineMatrix, 0, dataLineMatrix.Count - 1, 0);

            bool b = false;
            var same = 0;
            var from = 0;

            for (var d = 0; d <= dataLineMatrix.Count - 2; d++)
            {
                var firstFragment = dataLineMatrix[d].Fragments[0];
                var secondFragment = dataLineMatrix[d + 1].Fragments[0];

                if (firstFragment.Type != FragmentType.Empty && secondFragment.Type != FragmentType.Empty)
                {
                    if (firstFragment.Value.ToString() == secondFragment.Value.ToString())
                    {
                        if (b == false)
                        {
                            from = d;
                        }
                        same++;
                        b = true;
                    }
                    else
                    {
                        if (b == true)
                        {
                            Compare(dataLineMatrix, from, from + same, 1);
                            b = false;
                            same = 0;
                        }
                    }

                    if (b == true && d == dataLineMatrix.Count - 2)
                    {
                        Compare(dataLineMatrix, from, from + same, 1);
                        b = false;
                        same = 0;
                    }


                }
            }


            return dataLineMatrix;
        }

        private bool changed = false;

        private void Compare(IList<DataLine> dataLineMatrix, int from, int to, int column)
        {

            if (column >= MaxFragmentCount)
            {
                return;
            }

            Sort(dataLineMatrix, from, to, column);

            bool b = false;
            var same = 0;
            var fromLocal = 0;

            for (var d = from; d < to; d++)
            {
                var firstFragment = dataLineMatrix[d].Fragments[column];
                var secondFragment = dataLineMatrix[d + 1].Fragments[column];

                if (firstFragment.Type != FragmentType.Empty && secondFragment.Type != FragmentType.Empty)
                {



                    if (firstFragment.Value.ToString() == secondFragment.Value.ToString())
                    {
                        if (b == false)
                        {
                            fromLocal = d;
                        }
                        same++;
                        b = true;
                    }
                    else
                    {
                        if (b == true)
                        {
                            Compare(dataLineMatrix, fromLocal, fromLocal + same, column + 1);
                            b = false;
                            same = 0;
                        }
                    }

                    if (b == true && d == to - 1)
                    {
                        Compare(dataLineMatrix, fromLocal, fromLocal + same, column + 1);
                        b = false;
                        same = 0;
                    }
                }
            }
        }

        private void Sort(IList<DataLine> dataMatrix, int from, int to, int column)
        {
            changed = false;

            for (var i = from; i <= to - 1; i++)
            {
                Fragment f = null;
                Fragment f1 = null;

                f = dataMatrix[i].Fragments[column];
                f1 = dataMatrix[i + 1].Fragments[column];



                if (f1.Type == FragmentType.Empty && f.Type != FragmentType.Empty)
                {
                    var temp = dataMatrix[i];
                    dataMatrix[i] = dataMatrix[i + 1];
                    dataMatrix[i + 1] = temp;
                    changed = true;
                }
                else if (f.Type == FragmentType.Number && f1.Type == FragmentType.Number)
                {
                    if (decimal.Parse(f.Value.ToString()) > decimal.Parse(f1.Value.ToString()))
                    {
                        var temp = dataMatrix[i];
                        dataMatrix[i] = dataMatrix[i + 1];
                        dataMatrix[i + 1] = temp;
                        changed = true;
                    }
                }
                else if (f.Type == FragmentType.Word && f1.Type == FragmentType.Word)
                {
                    if (string.Compare(f.Value.ToString(), f1.Value.ToString(), StringComparison.Ordinal) > 0)
                    {

                        var temp = dataMatrix[i];
                        dataMatrix[i] = dataMatrix[i + 1];
                        dataMatrix[i + 1] = temp;
                        changed = true;
                    }

                }
                else if (f1.Type == FragmentType.Number && (f.Type == FragmentType.Word))
                {
                    var temp = dataMatrix[i];
                    dataMatrix[i] = dataMatrix[i + 1];
                    dataMatrix[i + 1] = temp;
                    changed = true;
                }

                if (changed == true)
                {
                    Sort(dataMatrix, from, to, column);
                }
            }

        }
    }
}