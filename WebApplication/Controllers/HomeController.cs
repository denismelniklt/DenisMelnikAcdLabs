using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewModels;

using IDataLineManagerBll = BLL.IDataLineManager;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataLineManagerBll DataLineManagerBll;

        public HomeController(IDataLineManagerBll dataLineManagerBll)
        {
            DataLineManagerBll = dataLineManagerBll;
        }

        public IActionResult Index()
        {
            var sortedDataLines = DataLineManagerBll.GetSortedDataLines(ApplicationEnvironment.InputFilePath);

            var dataLineViewModels = new List<DataLineViewModel>();

            if (sortedDataLines == null)
            {
            }
            else
            {
                foreach (var dataLine in sortedDataLines)
                {
                    var dataLineViewModel = new DataLineViewModel();

                    dataLineViewModel = Mapper.Map<DataLineViewModel>(dataLine);

                    dataLineViewModels.Add(dataLineViewModel);
                }

            }

            return View(dataLineViewModels);
        }
    }
}