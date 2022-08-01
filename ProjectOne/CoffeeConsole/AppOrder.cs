using System.Text;

namespace CoffeeConsole
{
    public class AppOrder
    {
        public string userName { get; set; }
        public int orderId { get; set; }
        public List<Drink> drinks { get; set; }

        public AppOrder() { }

        public AppOrder(string userName, List<Drink> drinks, int orderId)
        {
            this.userName = userName;
            this.drinks = drinks;
            this.orderId = orderId;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            double total = 0;
            sb.Append($"\n{userName}              Order number:{orderId}");
            
            foreach(Drink drink in this.drinks)
            {
                total += drink.price;
                sb.Append($"\n"+ drink.ToString());    
            }
            
            sb.Append($"\nTotal items:{drinks.Count}              Order total:{total}");

            return sb.ToString();
        }
    }
}