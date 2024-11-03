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
    abstract class IceCream
    {
        private string option;
        private int scoops;
        private List<Flavour> flavours;
        private List<Topping> toppings;

        public string Option { get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; }
        public List<Topping> Toppings { get; set; }

        public IceCream() { }

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            string flavs = "";
            for (int i =0; i < Flavours.Count; i++)
            {
                flavs += $"\n{Flavours[i].ToString()}";
            }
            string tops = "";
            for (int i = 0; i < Toppings.Count; i++)
            {
                tops += $"\n{Toppings[i].ToString()}";
            }
            return $"Option: {Option} \tScoops: {Scoops} {flavs} {tops}";
        }
    }
}
