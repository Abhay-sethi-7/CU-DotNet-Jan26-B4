using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinTrackPro.Data;
using FinTrackPro.Models;

namespace FinTrackPro.Controllers
{
    public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ
    public IActionResult Index()
    {
        var accounts = _context.Accounts.ToList();
        return View(accounts);
    }

    // CREATE FORM
    public IActionResult Create()
    {
        return View();
    }

    // WRITE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account account)
    {
        if (ModelState.IsValid)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            TempData["Success"] = "Account Created Successfully";
            return RedirectToAction("Index");
        }

        return View(account);
    }
}
   
    }

