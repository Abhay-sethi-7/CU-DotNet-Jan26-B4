namespace HeightClass
{
    class Height
    {
        public int Feet { get; set; }
        public double Inches { get; set; }


        public Height()
        {
            Feet = 0;
            Inches = 0.0;
        }
        public Height(int Feet, double Inches)
        {
              this.Feet=Feet;
             this.Inches=Inches ;
        }
       public Height(double inches)
        {
            this.Feet = (int)inches / 12;
            this.Inches = inches % 12;
        }

        public Height AddHeights(Height h)
        {
            int totalFeet = this.Feet + h.Feet;
            double totalInches = this.Inches + h.Inches;

            if (totalInches >= 12)
            {
                totalFeet = totalFeet +(int)(totalInches / 12);
                totalInches = totalInches % 12;

            }

            return new Height(totalFeet, totalInches);
        }
        public override string ToString()
        {
            return $" Height is {Feet} feet {Inches:F2} inches";
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Height h1 = new Height(5, 8.5);
            Height h2 = new Height(4, 7.8);
            Height h3 = new Height(123.4);
            Console.WriteLine($"Height 1:{h1} ");
            Console.WriteLine($"Height 2:{h2} ");
            Console.WriteLine($"Height 3:{h3} ");
            Console.WriteLine();
            Height totalHeight = h1.AddHeights(h2);

            Console.WriteLine(totalHeight);
        }
    }
}
