// Student Number : S10257108
// Student Name : Jodie Yap
// Partner Name : Ayden See

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10257108_PRG2Assignment
{
    internal class Cup : IceCream
    {
        public Cup() { }
        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings) : base(option, scoops, flavours, toppings) { }

        public override double CalculatePrice()
        {
            double price = 0.0;

            string[] optline = File.ReadAllLines("options.csv");
            for (int i = 1; i < 4; i++)
            {
                string[] info = optline[i].Split(',');
                if (Scoops == Convert.ToInt32(info[1]))
                {
                    price += Convert.ToDouble(info[4]);
                }
            }
            foreach (Flavour flavour in Flavours)
            {
                if (flavour.Premium == true)
                {
                    price += 2;
                }
            }
            price += Toppings.Count();
            return price;
        }
        public override string ToString()
        {
            double pr = CalculatePrice();
            return base.ToString() + $" \nPrice: ${pr}";
        }
    }
}
