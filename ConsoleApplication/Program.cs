using AutoMapper;
using AutoMapper.Mappers;
using Domain;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using ViewModels;

using DataLineManagerBll = BLL.DataLineManager;
using DataLineManagerDal = DAL.DataLineManager;
using IDataLineManagerBll = BLL.IDataLineManager;
using IDataLineManagerDal = DAL.IDataLineManager;

namespace ConsoleApplication
{
    class Program
    {
        private const string InputFilePath = "input.txt";
        private static IDataLineManagerBll DataLineManagerBll { get; set; }

        static void Main(string[] args)
        {
            RegisterServices();
            InitializeAutomapper();

            var dataLines = DataLineManagerBll.GetSortedDataLines(InputFilePath);
            ShowMatrix(dataLines);

            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<IDataLineManagerDal, DataLineManagerDal>()
                .AddTransient<IDataLineManagerBll, DataLineManagerBll>()                
                .BuildServiceProvider();

            DataLineManagerBll = serviceProvider.GetService<IDataLineManagerBll>();
        }

        private static void InitializeAutomapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddConditionalObjectMapper().Where((s, d) => s.Name == d.Name + "Dto");
            });
        }

        private static void ShowMatrix(IEnumerable<DataLine> dataLines)
        {
            var dataLineViewModels = new List<DataLineViewModel>();

            if (dataLines == null)
            {
            }
            else
            {
                foreach (var dataLine in dataLines)
                {
                    var dataLineViewModel = new DataLineViewModel();

                    dataLineViewModel = Mapper.Map<DataLineViewModel>(dataLine);

                    dataLineViewModels.Add(dataLineViewModel);
                }

            }

            foreach (var dataLineViewModel in dataLineViewModels)
            {
                Console.WriteLine(dataLineViewModel.GetString());
            }
        }
    }
}