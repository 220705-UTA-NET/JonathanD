namespace CoffeeConsole
{
    public class Order
    {
        public int orderId { get; set; }
        public int customerId { get; set; }

        public Order() { }

        public Order(int orderId, int customerId)
        {
            this.orderId = orderId;
            this.customerId = customerId;
        }
    }
}