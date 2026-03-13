using Microsoft.AspNetCore.Mvc;
using PulsePortal.Models;

namespace PulsePortal.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Dashboard()
        {
            List<Employee> employees = new List<Employee>()
        {
            new Employee{ Id = 1, Name="A", Position="Developer", Salary=50000 },
            new Employee{ Id = 2, Name="B", Position="Designer", Salary=45000 },
            new Employee{ Id = 3, Name="C", Position="Manager", Salary=65000 },
            new Employee{ Id = 4, Name="D", Position="Developer", Salary=55000 }
        };

            ViewBag.Announcement = "Team meeting at 2PM!!";

            ViewData["DepartmentName"] = "Software Development";
            ViewData["IsActive"] = true;

            return View(employees);
        }
    }
}
