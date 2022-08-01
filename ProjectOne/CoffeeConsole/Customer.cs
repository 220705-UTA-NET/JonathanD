namespace CoffeeConsole
{
    public class Customer
    {
        public int? customerId { get; set; }
        public string? userName { get; set; }
        

        public Customer() { }
        public Customer(int customerId, string userName)
        {
            this.customerId = customerId;
            this.userName = userName;
        }
    }
}