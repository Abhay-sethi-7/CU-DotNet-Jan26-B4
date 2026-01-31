namespace HospitalBillingEngine
{
    class Patient
    {
        public string Name { get; set; }
        public decimal BaseFees { get; set; }
        public Patient(string name, decimal baseFees)
        {
            if (string.IsNullOrWhiteSpace(name))
                Name = "Unknown";   
            else
                Name = name;

            if (baseFees < 0)
                BaseFees = 0;       
            else
                BaseFees = baseFees;
        }
        public virtual decimal CalculateFinalBill()
        {
            return BaseFees;
        }
    }
    class Inpatient : Patient
    {
        public int DaysStayed { get; set; }
        public decimal DailyRate { get; set; }

        public Inpatient(string name, decimal baseFee, int daysStayed, decimal dailyRate)
            : base(name, baseFee)
        {
            DaysStayed = daysStayed;
            DailyRate = dailyRate;
        }
        public override decimal CalculateFinalBill()
        {
            return BaseFees + (DaysStayed * DailyRate);
        }
    }
    class Outpatient : Patient
    {
        public decimal ProcedureFee { get; set; }

        public Outpatient(string name, decimal baseFee, decimal procedureFee)
            : base(name, baseFee)
        {
            ProcedureFee = procedureFee;
        }

        public override decimal CalculateFinalBill()
        {
            return BaseFees + ProcedureFee;
        }
    }
    class EmergencyPatient : Patient
    {
        private int severityLevel; 

        public int SeverityLevel
        {
            get { return severityLevel; }
            set
            {
                if (value < 1) severityLevel = 1;
                else if (value > 5) severityLevel = 5; 
                else severityLevel = value;
            } }

        public EmergencyPatient(string name, decimal baseFee, int severityLevel)
            : base(name, baseFee)
        {
            SeverityLevel = severityLevel;
        }

        public override decimal CalculateFinalBill()
        {
            return BaseFees * SeverityLevel;
        }
    }

    class HospitalBilling
    {
        private List<Patient> patients = new List<Patient>();

        public void AddPatient(Patient p)
        {
            patients.Add(p);
        }

        public void GenerateDailyReport()
        {
            Console.WriteLine("----- Daily Report -----");
            foreach (var p in patients)
            {
                Console.WriteLine($"{p.Name} : {p.CalculateFinalBill():C2}");
            }
        }

        public decimal CalculateTotalRevenue()
        {
            decimal total = 0;
            foreach (var p in patients)
            {
                total += p.CalculateFinalBill();
            }
            return total;
        }

        public int GetInpatientCount()
        {
            int count = 0;
            foreach (var p in patients)
            {
                if (p is Inpatient) count++;
            }
            return count;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            HospitalBilling billing = new HospitalBilling();

            billing.AddPatient(new Inpatient("Abhay", 500, 3, 200));
            billing.AddPatient(new Outpatient("Rahul", 300, 150));
            billing.AddPatient(new EmergencyPatient("Nahida", 400, 4));

            billing.GenerateDailyReport();

            Console.WriteLine("----------------------");
            Console.WriteLine("Total Revenue: " + billing.CalculateTotalRevenue().ToString("C2"));

            Console.WriteLine("Number of Inpatients: " + billing.GetInpatientCount());
        }
    }
}