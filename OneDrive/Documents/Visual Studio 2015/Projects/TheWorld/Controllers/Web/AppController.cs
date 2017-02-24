using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    // Controller is base for all controllers 
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private WorldContext _context;

        // mail service injection through constructor-- uses mailservice interface
        // get config from constructor but store in class level to use it
        public AppController(IMailService mailService, IConfigurationRoot config, WorldContext context)
        {
            _mailService = mailService;
            _config = config;
            _context = context;
        }
        //IActionResult--> outputs different kinds of result
        
        public IActionResult Index()
        {
            //convert all trips to list and send it to view
            //
            var data = _context.Trips.ToList();
               // throw new InvalidOperationException("bad things happen to good developers");

            // all views in App(name of controller) and name of file matches action(Index here)
            return View(data);
        }

        public IActionResult Contact()
        {
          //  throw new InvalidOperationException("bad things happen to good developers");
            return View();
        }

        //this method for post
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("", "we dont support aol addresses");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSetting:ToAddress"], model.Email, "From TheWorld", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
