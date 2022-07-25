namespace CoffeeService.Model
{
    public class Customer
    {
        public int customerId { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }

        public Customer() { }
        public Customer(int customerId, string fname, string lname)
        {
            this.customerId = customerId;
            this.fname = fname;
            this.lname = lname;
        }
    }
}