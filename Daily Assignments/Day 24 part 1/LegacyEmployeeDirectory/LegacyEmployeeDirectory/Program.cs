using System.Collections;
   namespace LegacyEmployeeDirectory
{
    
    class Program
    {
        static void Main()
        {
           
            Hashtable employeeTable = new Hashtable();

            employeeTable.Add(101, "A");
            employeeTable.Add(102, "B");
            employeeTable.Add(103, "C");
            employeeTable.Add(104, "D");

           
            if (!employeeTable.ContainsKey(105))
            {
                employeeTable.Add(105, "E");
            }
            else
            {
                Console.WriteLine("ID already exists.");
            }

            object emp102_Obj = employeeTable[102];  
            string emp102_Name = (string)emp102_Obj;  

            Console.WriteLine($"Employee 102 Name: {emp102_Name}");
            Console.WriteLine();

            Console.WriteLine("Employee Records:");
            foreach (DictionaryEntry emp in employeeTable)
            {
                Console.WriteLine($"ID: {emp.Key}, Name: {emp.Value}");
            }

            Console.WriteLine();

            employeeTable.Remove(103);

            Console.WriteLine($"Total Employees after removal: {employeeTable.Count}");
        }
    }

}
