namespace OrderReport
{
    class Order
    {

        private int _orderId;
        private string _customerName;
        private decimal _totalAmount;
        private DateTime _orderDate;
        private string _orderStatus;
        private bool _discountApplied;


        public Order()
        {
            _orderDate = DateTime.Now;
            _orderStatus = "NEW";
            _totalAmount = 0;
            _discountApplied = false;
        }

        public Order(int orderId, string customerName)
        {
            this._orderId = orderId;
            this.CustomerName = customerName;
            _orderDate = DateTime.Now;
            _orderStatus = "NEW";
            _totalAmount = 0;
            _discountApplied = false;
        }

        public int OrderId
        {
            get { return _orderId; }
        }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    _customerName = value;
            }
        }

        public decimal TotalAmount
        {
            get { return _totalAmount; }
        }

        public void AddItem(decimal price)
        {
            if (price > 0)
                _totalAmount += price;
        }

        public void ApplyDiscount(decimal percentage)
        {
            if (!_discountApplied && percentage >= 1 && percentage <= 30)
            {
                _totalAmount -= _totalAmount * percentage / 100;

                if (_totalAmount < 0)
                    _totalAmount = 0;

                _discountApplied = true;
            }
        }

        public string GetOrderSummary()
        {
            return
                $"Order Id: {_orderId}\n" +
                $"Customer: {_customerName}\n" +
                $"Total Amount: {_totalAmount}\n" +
                $"Status: {_orderStatus}";
        }
    }

    internal class Program
    {
        static void Main()
        {
            Order order1 = new Order(101, "Rahul");
           
            order1.AddItem(500);
            order1.AddItem(300);
            order1.ApplyDiscount(10);

            Console.WriteLine(order1.GetOrderSummary());
            Order order2 = new Order(102, "Amit");

            order2.AddItem(800);
            order2.AddItem(300);
            order2.ApplyDiscount(10);

            Console.WriteLine(order2.GetOrderSummary());
        }
    }


}
