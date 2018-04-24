using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using DataLineManagerBll = BLL.DataLineManager;
using DataLineManagerDal = DAL.DataLineManager;
using IDataLineManagerBll = BLL.IDataLineManager;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        private const string FilePath = "input.txt";
        private IDataLineManagerBll DataLineManagerBll;

        [TestInitialize]
        public void Initialize()
        {
            var dataLineManagerDal = new DataLineManagerDal();
            DataLineManagerBll = new DataLineManagerBll(dataLineManagerDal);

            InitializeAutomapper();
        }

        [TestMethod]
        public void GetSortedDataLines()
        {
            var dataLines = DataLineManagerBll.GetSortedDataLines(FilePath);

            Assert.AreEqual(dataLines.Count(), 8);

            var expectedDataLineIndex = 4;
            var expectedFragmentIndex = 0;
            var expectedValue = -1.1M;

            var actualValueString = dataLines[expectedDataLineIndex].Fragments[expectedFragmentIndex].Value.ToString();
            decimal.TryParse(actualValueString, out decimal actualValue);

            Assert.AreEqual(actualValue, expectedValue);
        }

        private void InitializeAutomapper()
        {
            try
            {
                // Mapper cannot be initialized more than once
                Mapper.Initialize(cfg =>
                {
                    cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
                    cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "ViewModel");
                });
            }
            catch
            {
            }
        }
    }
}