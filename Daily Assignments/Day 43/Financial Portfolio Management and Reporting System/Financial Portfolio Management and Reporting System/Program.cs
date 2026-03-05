using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Financial_Portfolio_Management_and_Reporting_System
{
    // Custom Exception   
    public class InvalidFinancialDataException : Exception
    {
        public InvalidFinancialDataException(string message) : base(message) { }
    }

    // Interfaces
 
    public interface IRiskAssessable
    {
        string GetRiskCategory();
    }

    public interface IReportable
    {
        string GenerateReportLine();
    }

  
    // Abstract Base Class
    public abstract class FinancialInstrument
    {
        private decimal quantity;
        private decimal purchasePrice;
        private decimal marketPrice;
        public string InstrumentId { get; set; }
        public string Name { get; set; }
        private string currency;
        public string Currency
        {
            get => currency;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
                    throw new InvalidFinancialDataException("Currency must be a 3-letter code.");
                currency = value.ToUpper();
            }
        }

        public DateTime PurchaseDate { get; set; }

        public decimal Quantity
        {
            get => quantity;
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Quantity cannot be negative.");
                quantity = value;
            }
        }

        public decimal PurchasePrice
        {
            get => purchasePrice;
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Purchase price cannot be negative.");
                purchasePrice = value;
            }
        }

        public decimal MarketPrice
        {
            get => marketPrice;
            set
            {
                if (value < 0)
                    throw new InvalidFinancialDataException("Market price cannot be negative.");
                marketPrice = value;
            }
        }

        public abstract decimal CalculateCurrentValue();

        public virtual string GetInstrumentSummary()
        {
            return $"{InstrumentId} - {Name} - {CalculateCurrentValue():C}";
        }
    }
    // Instrument Implementations
   

    public class Equity : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
            => Quantity * MarketPrice;

        public string GetRiskCategory() => "High";

        public string GenerateReportLine()
            => $"Equity | {Name} | {CalculateCurrentValue():C} | Risk: {GetRiskCategory()}";
    }

    public class Bond : FinancialInstrument, IRiskAssessable, IReportable
    {
        public decimal InterestRate { get; set; }

        public override decimal CalculateCurrentValue()
            => Quantity * MarketPrice;

        public string GetRiskCategory() => "Medium";

        public string GenerateReportLine()
            => $"Bond | {Name} | {CalculateCurrentValue():C} | Risk: {GetRiskCategory()}";
    }

    public class MutualFund : FinancialInstrument, IRiskAssessable, IReportable
    {
        public override decimal CalculateCurrentValue()
            => Quantity * MarketPrice;

        public string GetRiskCategory() => "Moderate";

        public string GenerateReportLine()
            => $"MutualFund | {Name} | {CalculateCurrentValue():C} | Risk: {GetRiskCategory()}";
    }

    public class FixedDeposit : FinancialInstrument, IReportable
    {
        public decimal InterestRate { get; set; }
        public int TenureMonths { get; set; }

        public override decimal CalculateCurrentValue()
        {
            return PurchasePrice +
                   (PurchasePrice * InterestRate * TenureMonths / 12);
        }

        public string GenerateReportLine()
            => $"FixedDeposit | {Name} | {CalculateCurrentValue():C}";
    }

    // Transactions
   

    public enum TransactionType { Buy, Sell }

    public class Transaction
    {
        public string TransactionId { get; set; }
        public string InstrumentId { get; set; }
        public TransactionType Type { get; set; }
        public int Units { get; set; }
        public DateTime Date { get; set; }
    }

    // Portfolio
    public class Portfolio
    {
        private List<FinancialInstrument> _instruments = new();
        private Dictionary<string, FinancialInstrument> _instrumentLookup = new();

        public List<FinancialInstrument> Instruments => _instruments;

        public void AddInstrument(FinancialInstrument instrument)
        {
            if (_instrumentLookup.ContainsKey(instrument.InstrumentId))
                throw new InvalidFinancialDataException("Duplicate Instrument ID.");

            if (_instruments.Any() &&
                _instruments.First().Currency != instrument.Currency)
                throw new InvalidFinancialDataException("Currency mismatch in portfolio.");

            _instruments.Add(instrument);
            _instrumentLookup[instrument.InstrumentId] = instrument;
        }

        public void RemoveInstrument(string id)
        {
            if (!_instrumentLookup.ContainsKey(id))
                throw new InvalidFinancialDataException("Instrument not found.");

            _instruments.Remove(_instrumentLookup[id]);
            _instrumentLookup.Remove(id);
        }

        public FinancialInstrument GetInstrumentById(string id)
        {
            if (!_instrumentLookup.ContainsKey(id))
                throw new InvalidFinancialDataException("Instrument not found.");

            return _instrumentLookup[id];
        }

        public List<FinancialInstrument> GetInstrumentsByRisk(string risk)
        {
            return _instruments
                .OfType<IRiskAssessable>()
                .Where(i => i.GetRiskCategory()
                .Equals(risk, StringComparison.OrdinalIgnoreCase))
                .Cast<FinancialInstrument>()
                .ToList();
        }

        public decimal GetTotalInvestment()
            => _instruments.Sum(i => i.PurchasePrice * i.Quantity);

        public decimal GetTotalPortfolioValue()
            => _instruments.Sum(i => i.CalculateCurrentValue());

        public void ProcessTransaction(Transaction txn)
        {
            if (!_instrumentLookup.ContainsKey(txn.InstrumentId))
                throw new InvalidFinancialDataException("Instrument not found.");

            if (txn.Units <= 0)
                throw new InvalidFinancialDataException("Units must be positive.");

            var instrument = _instrumentLookup[txn.InstrumentId];

            if (txn.Type == TransactionType.Buy)
            {
                instrument.Quantity += txn.Units;
            }
            else
            {
                if (instrument.Quantity < txn.Units)
                    throw new InvalidFinancialDataException("Cannot sell more than owned.");

                instrument.Quantity -= txn.Units;
            }
        }
    }

    // Reporting


    public class ReportGenerator
    {
        private Portfolio _portfolio;

        public ReportGenerator(Portfolio portfolio)
        {
            _portfolio = portfolio;
        }

        public void GenerateConsoleReport()
        {
            Console.WriteLine(" PORTFOLIO SUMMARY ");
            Console.WriteLine("-------------------");

            var grouped = _portfolio.Instruments.GroupBy(i => i.GetType().Name);

            foreach (var group in grouped)
            {
                decimal investment = group.Sum(i => i.PurchasePrice * i.Quantity);
                decimal current = group.Sum(i => i.CalculateCurrentValue());

                Console.WriteLine($"Instrument Type: {group.Key}");
                Console.WriteLine($"Total Investment: {investment:C}");
                Console.WriteLine($"Current Value: {current:C}");
                Console.WriteLine($"Profit/Loss: {(current - investment):C}\n");
            }

            Console.WriteLine($"Overall Portfolio Value: {_portfolio.GetTotalPortfolioValue():C}\n");

            var riskGroups = _portfolio.Instruments
                .OfType<IRiskAssessable>()
                .GroupBy(i => i.GetRiskCategory());

            Console.WriteLine("Risk Distribution:");
            foreach (var risk in riskGroups)
                Console.WriteLine($"{risk.Key}: {risk.Count()}");
        }

        public void GenerateFileReport()
        {
            string fileName = $"../../../PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";

            try
            {
                using StreamWriter writer = new StreamWriter(fileName);

                writer.WriteLine("------- PORTFOLIO REPORT ---------");
                writer.WriteLine($"Generated On: {DateTime.Now}");
                writer.WriteLine();

                foreach (var instrument in _portfolio.Instruments
                    .OrderByDescending(i => i.CalculateCurrentValue()))
                {
                    writer.WriteLine(instrument.GetInstrumentSummary());
                }

                writer.WriteLine();
                writer.WriteLine($"Total Portfolio Value: {_portfolio.GetTotalPortfolioValue():C}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("File write permission error.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File Error: {ex.Message}");
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            try
            {
                Portfolio portfolio = new Portfolio();

                
                string csv = "EQ001,Equity,INFY,INR,100,1500,1650";
                string[] parts = csv.Split(',');

                if (parts.Length != 7)
                    throw new InvalidFinancialDataException("Invalid CSV format.");

                FinancialInstrument equity = new Equity
                {
                    InstrumentId = parts[0],
                    Name = parts[2],
                    Currency = parts[3],
                    Quantity = decimal.Parse(parts[4]),
                    PurchasePrice = decimal.Parse(parts[5]),
                    MarketPrice = decimal.Parse(parts[6]),
                    PurchaseDate = DateTime.Now.AddMonths(-12)
                };

                portfolio.AddInstrument(equity);

                // Transaction Array
                Transaction[] txnArray = new Transaction[2];

                txnArray[0] = new Transaction
                {
                    TransactionId = "T1",
                    InstrumentId = "EQ001",
                    Type = TransactionType.Buy,
                    Units = 10,
                    Date = DateTime.Now
                };

                txnArray[1] = new Transaction
                {
                    TransactionId = "T2",
                    InstrumentId = "EQ001",
                    Type = TransactionType.Sell,
                    Units = 5,
                    Date = DateTime.Now
                };

                List<Transaction> txnList = txnArray.Where(t => t != null).ToList();

                foreach (var txn in txnList)
                    portfolio.ProcessTransaction(txn);

                ReportGenerator report = new ReportGenerator(portfolio);

                report.GenerateConsoleReport();
                report.GenerateFileReport();
            }
            catch (InvalidFinancialDataException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}