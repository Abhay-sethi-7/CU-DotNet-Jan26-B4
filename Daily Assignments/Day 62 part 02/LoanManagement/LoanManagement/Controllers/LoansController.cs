using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoanManagement.Data;
using LoanManagement.Model;
using LoanManagement.DTO;

namespace LoanManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LoanManagementContext _context;

        public LoansController(LoanManagementContext context)
        {
            _context = context;
        }

        // GET: api/Loans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanReadDto>>> GetLoan()
        {
            var loans = await _context.Loan.ToListAsync();

            var result = loans.Select(l => new LoanReadDto
            {
                Id = l.Id,
                BorrowerName = l.borrowerName,
                Amount = l.amount,
                LoanTermMonths = l.loanTermMonths,
                IsApproved = l.isApproved
            });

            return Ok(result);
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanReadDto>> GetLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            var result = new LoanReadDto
            {
                Id = loan.Id,
                BorrowerName = loan.borrowerName,
                Amount = loan.amount,
                LoanTermMonths = loan.loanTermMonths,
                IsApproved = loan.isApproved
            };

            return Ok(result);
        }

        // POST: api/Loans
        [HttpPost]
        public async Task<ActionResult<LoanReadDto>> PostLoan(CreateLoanDto dto)
        {
            var loan = new Loan
            {
                borrowerName = dto.BorrowerName,
                amount = dto.Amount,
                loanTermMonths = dto.LoanTermMonths,
                isApproved = false // default
            };

            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();

            var result = new LoanReadDto
            {
                Id = loan.Id,
                BorrowerName = loan.borrowerName,
                Amount = loan.amount,
                LoanTermMonths = loan.loanTermMonths,
                IsApproved = loan.isApproved
            };

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, result);
        }

        // PUT: api/Loans/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoan(int id, UpdateLoanDto dto)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            // Update fields
            loan.borrowerName = dto.BorrowerName;
            loan.amount = dto.Amount;
            loan.loanTermMonths = dto.LoanTermMonths;
            loan.isApproved = dto.IsApproved;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null)
                return NotFound();

            _context.Loan.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}