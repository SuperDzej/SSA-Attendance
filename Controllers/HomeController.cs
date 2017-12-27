using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SSAAttenderAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok("Index page");
        }

        public IActionResult About()
        {
            return Ok("About page");
        }

        public IActionResult Contact()
        {
            return Ok("Contact page");
        }

        public IActionResult Error()
        {
            return BadRequest("Error page");
        }
    }
}
