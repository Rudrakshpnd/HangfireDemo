using Hangfire;
using HangfireDemo.Models;
using HangfireDemo.MyDbContext;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HangfireDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SourceDbContext _sourceDbContext;
        private readonly DestinationDbContext _destinationDbContext;

        public HomeController(SourceDbContext sourceDbContext, DestinationDbContext destinationDbContext, ILogger<HomeController> logger)
        {
            _logger = logger;
            _sourceDbContext = sourceDbContext;
            _destinationDbContext = destinationDbContext;
        }

       

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewJob()
        {
            // var id = BackgroundJob.Enqueue(()=>SendNotifiaction("Welcome to Demo"));

            //var id2 = BackgroundJob.Schedule(() => SendNotifiaction("Welcome to Demo"), TimeSpan.FromSeconds(15));


            RecurringJob.AddOrUpdate(() =>TransferData(), Cron.Minutely);
            return View();
        }







        public IActionResult TransferData()
        {
            try
            {
                // Retrieve data from the source database
                var sourceData = _sourceDbContext.SourceTable.ToList();


                List<DestinationDbContextModel> destinationData = sourceData
                .Select(sourceModel => new DestinationDbContextModel
                {
                    Id = sourceModel.Id,
                    Name = sourceModel.Name,
                    Description= sourceModel.Description,
                    Email = sourceModel.Email,
                })
                .ToList();

                // Insert data into the destination database
                _destinationDbContext.DestinationTable.AddRange(destinationData);
                _destinationDbContext.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to home page or another appropriate page

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }




        public void SendNotifiaction(string v)
        {
            Console.WriteLine(v);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}