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
    internal class Order
    {
        private int id;
        private DateTime timeReceived;
        private DateTime? timeFulfilled;
        private List<IceCream> iceCreamList;

        public int Id { get; set; }
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList { get; set; }

        public Order() { }
        public Order(int i, DateTime tr)
        {
            Id = i;
            TimeReceived = tr;
        }
        public void ModifyIceCream(int i)
        {
            int count = 0;
            foreach (IceCream ic in IceCreamList)
            {
                count++;
            }
            bool done = false;
            while (true)
            {
                try
                {
                    Console.Write("Choose an ice cream in the order for modification: ");
                    int num = Convert.ToInt16(Console.ReadLine());
                    if (num > 0 && num <= count)
                    {
                        int track = 0;
                        foreach (IceCream ic in IceCreamList)
                        {
                            track++;
                            if (track == num)
                            {
                                Console.WriteLine("\n1) Option\n2) Scoops\n3) Flavours\n4) Toppings\n5) Dipped Cone \n6) Waffle Flavour");
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Select what modification you would like to make: ");
                                        int input = Convert.ToInt32(Console.ReadLine());
                                        if (input > 0 && input <= 6)
                                        {
                                            if (input == 1)
                                            {
                                                while (true)
                                                {
                                                    Console.WriteLine("\n1) Waffle\n2) Cone\n3) Cup");
                                                    try
                                                    {
                                                        Console.Write("What option would you like to change to?: ");
                                                        int option = Convert.ToInt32(Console.ReadLine());
                                                        if (option > 0 && option <= 3)
                                                        {
                                                            if (option == 1)
                                                            {
                                                                ic.Option = "Waffle";
                                                                Console.WriteLine("Modifications made successfully!");
                                                            }
                                                            else if (option == 2)
                                                            {
                                                                ic.Option = "Cone";
                                                                Console.WriteLine("Modifications made successfully!");
                                                            }
                                                            else if (option == 3)
                                                            {
                                                                ic.Option = "Cup";
                                                                Console.WriteLine("Modifications made successfully!");
                                                            }
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Please input a value within the provided range.");
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                            }
                                            else if (input == 2)
                                            {
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        Console.Write("How many scoops to change to? [1/2/3]: ");
                                                        int option = Convert.ToInt32(Console.ReadLine());
                                                        if (option > 0 && option <= 3)
                                                        {
                                                            ic.Scoops = option;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Please input a value within the provided range.");
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                            }
                                            else if (input == 5)
                                            {
                                                if (ic.Option == "Cone")
                                                {
                                                    try
                                                    {
                                                        Console.WriteLine("\n1) Dipped\n2) Not Dipped");
                                                        Console.Write("Select an option: ");
                                                        int dips = Convert.ToInt32(Console.ReadLine());
                                                        if (dips == 1 || dips == 2)
                                                        {
                                                            if (dips == 1)
                                                            {
                                                                Cone cone = new Cone(ic.Option, ic.Scoops, ic.Flavours, ic.Toppings, true);
                                                                IceCreamList.Remove(ic);
                                                                IceCreamList.Add(cone);
                                                            }
                                                            else
                                                            {
                                                                Cone cone = new Cone(ic.Option, ic.Scoops, ic.Flavours, ic.Toppings, false);
                                                                IceCreamList.Remove(ic);
                                                                IceCreamList.Add(cone);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Please input a value within the given range.");
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Your chosen option does not provide dipping as only cones can opt for dipping.");
                                                }
                                                break;
                                            }
                                            else if (input == 6)
                                            {
                                                if (ic.Option == "Waffle")
                                                {
                                                    try
                                                    {
                                                        Console.WriteLine("\n1) Red Velvet\n2) Charcoal \nPandan Waffle");
                                                        Console.Write("Select an option: ");
                                                        int wafflav = Convert.ToInt32(Console.ReadLine());
                                                        Waffle waffle = null;
                                                        if (wafflav >= 1 && wafflav <= 3)
                                                        {
                                                            if (wafflav == 1)
                                                            {
                                                                waffle = new Waffle(ic.Option, ic.Scoops, ic.Flavours, ic.Toppings, "Red Velvet");
                                                                IceCreamList.Remove(ic);
                                                                IceCreamList.Add(waffle);
                                                            }
                                                            if (wafflav == 2)
                                                            {
                                                                waffle = new Waffle(ic.Option, ic.Scoops, ic.Flavours, ic.Toppings, "Charcoal");
                                                                IceCreamList.Remove(ic);
                                                                IceCreamList.Add(waffle);
                                                            }
                                                            if (wafflav == 3)
                                                            {
                                                                waffle = new Waffle(ic.Option, ic.Scoops, ic.Flavours, ic.Toppings, "Pandan Waffle");
                                                                IceCreamList.Remove(ic);
                                                                IceCreamList.Add(waffle);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Please input a value within the given range.");
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Your chosen option is invalid as only waffles can have flavours.");
                                                }
                                                break;
                                            }
                                            else if (input == 4)
                                            {
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        List<Topping> toppings = new List<Topping>();
                                                        Console.Write("How many toppings to change to? [1/2/3/4]: ");
                                                        int option = Convert.ToInt32(Console.ReadLine());
                                                        if (option > 0 && option <= 4)
                                                        {
                                                            for (i = 1; i <= option; i++)
                                                            {
                                                                while (true)
                                                                {
                                                                    Console.WriteLine("Enter topping: ");
                                                                    string topping = Console.ReadLine();
                                                                    if (topping.ToLower() != "sprinkles" || topping.ToLower() != "mochi" || topping.ToLower() != "sago" || topping.ToLower() != "oreos")
                                                                    {
                                                                        continue;
                                                                    }
                                                                    Topping top = new Topping(topping);
                                                                    toppings.Add(top);
                                                                    break;
                                                                }
                                                            }
                                                            ic.Toppings = toppings;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Please input a value within the provided range.");
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                            }
                                            else if (input == 3)
                                            {
                                                while (true)
                                                {
                                                    try
                                                    {
                                                        List<Flavour> flavours = new List<Flavour>();
                                                        for (i = 0; i < ic.Scoops; i++)
                                                        {
                                                            while (true)
                                                            {
                                                                Console.WriteLine("Enter flavour for this scoop: ");
                                                                string flav = Console.ReadLine();
                                                                if (flav.ToLower() != "strawberry" || flav.ToLower() != "chocolate" || flav.ToLower() != "vanilla" || flav.ToLower() != "ube" || flav.ToLower() != "durian" || flav.ToLower() != "sea salt")
                                                                {
                                                                    continue;
                                                                }
                                                                bool prem = false;
                                                                if (flav.ToLower() == "ube" || flav.ToLower() == "durian" || flav.ToLower() == "sea salt")
                                                                {
                                                                    prem = true;
                                                                }
                                                                Flavour flavour = new Flavour(flav, prem);
                                                                flavours.Add(flavour);
                                                                break;
                                                            }
                                                        }
                                                        ic.Flavours = flavours;
                                                        break;
                                                    }
                                                    catch (Exception)
                                                    {
                                                        Console.WriteLine("Please enter a valid option.");
                                                    }
                                                }
                                            }
                                            done = true;
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please input a value within a provided range.");
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter a valid input.");
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please input a value within the range provided.");
                    }
                    if (done)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please input a valid option.");
                }
            }
        }
        public void AddIceCream(IceCream ic)
        {
            IceCreamList.Add(ic);
        }
        public void DeleteIceCream(int i)
        {
            iceCreamList.Remove(iceCreamList[i-1]);
        }
        public double CalculateTotal()
        {
            double totalcost = 0;
            foreach (IceCream i in iceCreamList)
            {
                totalcost += i.CalculatePrice();
            }
            return totalcost;
        }
        public string ToString()
        {
            return $"ID: {Id}" +
                $"\tTime Received: {TimeReceived}" +
                $"\tTime Fulfilled: {TimeFulfilled}";
        } 
    }
}
