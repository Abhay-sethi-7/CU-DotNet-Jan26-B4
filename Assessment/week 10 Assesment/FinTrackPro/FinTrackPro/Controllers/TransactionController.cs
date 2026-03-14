using FinTrackPro.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class TransactionController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

   
    public IActionResult Index()
    {
        var transactions = _context.Transactions
            .Include(t => t.Account)
            .ToList();

        return View(transactions);
    }

  
}