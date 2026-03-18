using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickLoan.Models;

namespace QuickLoan.Controllers
{
    public class LoanController : Controller
    {
        private static List<Loan> loans = new List<Loan>();
        
        public ActionResult Index()
        {
            return View(loans);
        }

        // GET: LoanController/Details
        public IActionResult Details(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan == null)
                return NotFound();

            return View(loan);
        }

        // GET: LoanController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.Id = loans.Count + 1;
                loans.Add(loan);

                return RedirectToAction(nameof(Index));
            }

            return View(loan);
        }

        // GET: LoanController/Edit/5
        public ActionResult Edit(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);
            return View(loan);
        }

        // POST: LoanController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Loan loan)
        {
            var existingLoan = loans.FirstOrDefault(x => x.Id == loan.Id);

            if (existingLoan != null)
            {
                existingLoan.BorrowerName = loan.BorrowerName;
                existingLoan.LenderName = loan.LenderName;
                existingLoan.Amount = loan.Amount;
                existingLoan.IsSettled = loan.IsSettled;
            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var loan = loans.FirstOrDefault(x => x.Id == id);

            if (loan != null)
                loans.Remove(loan);

            return RedirectToAction(nameof(Index));
        }
    }
}
