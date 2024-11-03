// Student Number : S10257108
// Student Name : Jodie Yap
// Partner Name : Ayden See


using S10257108_PRG2Assignment;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

List<Customer> customerhist = new List<Customer>();
(Queue<Order> goldQueue, Queue<Order> normalQueue, Queue<Order> totalQueue) = Queues();
void pastOrders() //creates the orderhistory for every customer if applicable

{
    string[] orderI = File.ReadAllLines("orders.csv");
    for (int i = 1; i < orderI.Length; i++)
    {
        string[] orders = orderI[i].Split(',');
        foreach (Customer c in customerhist)
        {
            if (c.MemberId == Convert.ToInt32(orders[1]))
            {
                foreach (Order o in totalQueue)
                {
                    if (Convert.ToInt32(orders[0]) == o.Id)
                    {
                        List<Flavour> flavours = new List<Flavour>();
                        List<Topping> toppings = new List<Topping>();
                        List<IceCream> icl = new List<IceCream>();
                        for (int f = 8; f < 11; f++)
                        {
                            if (orders[f] == "")
                            {
                                break;
                            }
                            bool premium = Prem(orders[f]);
                            Flavour flavour1 = new Flavour(orders[f], premium);
                            flavours.Add(flavour1);
                        }
                        for (int t = 11; t < 15; t++)
                        {
                            if (orders[t] == "")
                            {
                                break;
                            }
                            Topping topping1 = new Topping(orders[t]);
                            toppings.Add(topping1);
                        }
                        IceCream icecr = null;
                        if (orders[4] == "Waffle")
                        {
                            icecr = new Waffle(orders[4], (Convert.ToInt16(orders[5])), flavours, toppings, orders[7]);
                        }
                        else if (orders[4] == "Cup")
                        {
                            icecr = new Cup(orders[4], (Convert.ToInt16(orders[5])), flavours, toppings);
                        }
                        else if (orders[4] == "Cone")
                        {
                            bool dipped = false;
                            if (orders[6] == "TRUE")
                            {
                                dipped = true;
                            }
                            icecr = new Cone(orders[4], (Convert.ToInt16(orders[5])), flavours, toppings, dipped);
                        }
                        bool check = false;
                        foreach (Order or in c.OrderHistory)
                        {
                            if (or.Id == Convert.ToInt32(orders[0]))
                            {
                                or.IceCreamList.Add(icecr);
                                check = true;
                                break;
                            }
                        }
                        if (check == false)
                        {
                            DateTime tf = Convert.ToDateTime(orders[3]);
                            o.TimeFulfilled = tf;
                            o.IceCreamList.Add(icecr);
                            c.OrderHistory.Add(o);
                        }
                        break;
                    }
                }
            }
        }
    }
}
(Queue<Order>, Queue<Order>, Queue<Order>) Queues() //creates 3 queues
{
    List<Customer> goldmember = GoldMembers();
    string[] orderInfo = File.ReadAllLines("orders.csv");
    List<Order> orderList = new List<Order>();
    Queue<Order> normalQ = new Queue<Order>();
    Queue<Order> goldQ = new Queue<Order>();
    Queue<Order> totalQ = new Queue<Order>();

    for (int i = 1; i < orderInfo.Length; i++)
    {
        string[] orders = orderInfo[i].Split(',');
        DateTime tr = Convert.ToDateTime(orders[2]);
        DateTime tf = Convert.ToDateTime(orders[3]);
        Order order = new Order(Convert.ToInt32(orders[0]), tr);
        order.TimeFulfilled = tf;
        order.IceCreamList = new List<IceCream>();
        orderList.Add(order);
        foreach (Customer c in goldmember)
        {
            if (Convert.ToInt32(orders[1]) == c.MemberId)
            {
                goldQ.Enqueue(order);
            }
        }
        if (goldQ.Contains(order) == false)
        {
            normalQ.Enqueue(order);
        }
        totalQ.Enqueue(order);
    }
    return (goldQ, normalQ, totalQ);
}
List<Customer> GoldMembers() //creates a list of gold members
{
    List<Customer> goldmember = new List<Customer>();
    string[] customerInfo = File.ReadAllLines("customers.csv");
    for (int i = 1; i < customerInfo.Length; i++)
    {
        string[] info = customerInfo[i].Split(',');
        {
            if (info[3] == "Gold")
            {
                DateTime dob = Convert.ToDateTime(info[2]);
                Customer customer = new Customer(info[0], Convert.ToInt32(info[1]), dob.Date);
                goldmember.Add(customer);
            }
        }
    }
    return goldmember;
}
void customerList() //creates a customer list
{
    string[] customerInfo = File.ReadAllLines("customers.csv");
    for (int i = 1; i < customerInfo.Length; i++)
    {
        string[] info = customerInfo[i].Split(',');
        DateTime dob = Convert.ToDateTime(info[2]);
        Customer customer = new Customer(info[0], Convert.ToInt32(info[1]), dob.Date);
        customer.OrderHistory = new List<Order>();
        customerhist.Add(customer);
    }
}
static bool Prem(string flav) //returns bool value on whether flavour is premium or not
{
    string[] flavline = File.ReadAllLines("flavours.csv");
    bool premium = false;
    for (int i = 1; i < flavline.Length; i++)
    {
        string[] info = flavline[i].Split(',');
        string type = info[0].ToLower();
        if (type == flav.ToLower())
        {
            if (Convert.ToInt16(info[1]) == 2)
            {
                premium = true;
            }
        }
    }
    return premium;
}

static bool Top(string toppin)
{
    string[] topline = File.ReadAllLines("toppings.csv");
    for (int i = 1; i < topline.Length; i++)
    {
        string[] info = topline[i].Split(',');
        string type = info[0];
        if (type == toppin)
        {
            return true;
        }
    }
    return false;
}

Dictionary<int, Customer> custdict() //creates a dictionary where i store the customer objects, to be used in the other parts
{
    Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
    string[] lines = File.ReadAllLines("customers.csv");
    for (int i = 1; i < lines.Length; i++)
    {
        string[] inf = lines[i].Split(',');
        Customer cust = new Customer(inf[0], Convert.ToInt32(inf[1]), Convert.ToDateTime(inf[2]));
        customers.Add(Convert.ToInt32(inf[1]), cust);
        PointCard card = new PointCard(Convert.ToInt32(inf[4]), Convert.ToInt32(inf[5]));
        card.Tier = inf[3];
        cust.Rewards = card;
    }
    return customers;
}

//Basic Feature 1 (Ayden)
void option1() //Reading the csv file as well as displaying all the information in the file
{
    string[] csvLines = File.ReadAllLines("customers.csv");
    Console.WriteLine("{0,-13}{1,-13}{2,-15}{3,-20}{4,-20}{5,-14}", "Name", "Member ID", "DOB", "Membership Status", "Membership Points", "Punch Card");
    for (int i = 1; i < csvLines.Length; i++)
    {
        string[] info = csvLines[i].Split(',');
        Console.WriteLine("{0,-13}{1,-13}{2,-15}{3,-20}{4,-20}{5,-14}", info[0], info[1], info[2], info[3], info[4], info[5]);
    }
}

//Basic Feature 2 (Jodie)
void option2()
{
    int countOfGoldPrinted = 0;
    Console.WriteLine();
    Console.WriteLine("Gold Queue:");
    foreach (Order o in goldQueue)
    {
        foreach (Customer cs in customerhist)
        {
            if (cs.CurrentOrder != null)
            {
                if (cs.CurrentOrder.Id == o.Id)
                {
                    Console.WriteLine(o.ToString());
                    foreach (IceCream ice in o.IceCreamList)
                    {
                        Console.WriteLine(ice);
                        Console.WriteLine();
                    }
                    countOfGoldPrinted++;
                    break;
                }
            }
        }
    }
    if (countOfGoldPrinted == 0)
    {
        Console.WriteLine("There are currently no current orders in the gold queue.");

    }
    int countOfNormalPrinted = 0;
    Console.WriteLine();
    Console.WriteLine("Normal Queue: ");
    foreach (Order o in normalQueue)
    {
        foreach (Customer cs in customerhist)
        {
            if (cs.CurrentOrder != null)
            {
                if (cs.CurrentOrder.Id == o.Id)
                {
                    Console.WriteLine(o.ToString());
                    foreach (IceCream ice in o.IceCreamList)
                    {
                        Console.WriteLine(ice);
                        Console.WriteLine();
                    }
                    countOfNormalPrinted++;
                    break;
                }
            }
        }
    }
    if (countOfNormalPrinted == 0)
    {
        Console.WriteLine("There are currently no current orders in the normal queue.");
    }
}

//Basic Feature 3 (Ayden)
void option3() //Registering new person
{
    string name;
    string ident;
    int id;
    DateTime dob;
    while (true) //Getting string input
    {
        Console.Write("Input your name: ");
        name = Console.ReadLine().ToLower();
        if (name != "")
        {
            break;
        }
    }
    while (true) //inputting their member ID. If not an integer, or not a 6 digit int, throws an error
    {
        try
        {
            Console.Write("Input ID number: ");
            ident = Console.ReadLine();
            id = Convert.ToInt32(ident);
            if (ident.Length == 6)
            {
                bool check = true;
                foreach (Customer c3 in customerhist)
                {
                    if (c3.MemberId == id)
                    {
                        Console.WriteLine("This member ID is already registered. Enter a different ID.");
                        check = false;
                        break;
                    }
                }
                if (check == true)
                {
                    break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a 6 digit input.");
            }
        }
        catch
        {
            Console.WriteLine("Please enter a numerical 6 digit input.");
        }
    }
    while (true) //Inputs birthdate in specific format, if not correct format an error will be thrown
    {
        try
        {
            Console.Write("Enter date of birth (dd/MM/yyyy): ");
            dob = Convert.ToDateTime(Console.ReadLine());
            break;
        }
        catch
        {
            Console.WriteLine("Please enter a valid input in dd/MM/yyyy format.");
        }
    }

    Customer customer = new Customer(name, id, dob); //creating the new customer object
    PointCard pointCard = new PointCard(0, 0);
    customer.Rewards = pointCard;
    using (StreamWriter sw = new StreamWriter("customers.csv", true)) //writes all the information into the customer.csv file
    {
        sw.Write(Environment.NewLine);
        sw.WriteLine($"{name},{id},{dob:dd/MM/yyyy},{pointCard.Tier},{pointCard.Points},{pointCard.PunchCard}");
    }
    Console.WriteLine("Registration successful!");
}

/*//Basic Feature 4 (Ayden)
void option4() //retriving customer ID, and creating an order for the customer
{
    option1();
    int id;
    string ident;
    while (true) //ensures the customer ID entered is a valid input
    {
        try
        {
            Console.Write("Please enter customer ID: ");
            ident = Console.ReadLine();
            id = Convert.ToInt32(ident);
            if (ident.Length > 6)
            {
                throw new Exception("Please enter a valid 6 digit input.");
            }
            if (ident.Length < 6)
            {
                throw new Exception("Please enter a valid 6 digit input.");
            }
            break;
        }
        catch
        {
            Console.WriteLine("Please enter a valid input.");
        }
    }

    Dictionary<int, Customer> customers = custdict();
    Customer customer;
    while (true) //Checks if customer exsists using customer dictionary
    {
        try
        {
            customer = customers[id];
            break;
        }
        catch
        {
            Console.WriteLine("Please enter a valid input.");
        }

    }



    string another = "";
    while (another != "n") //if customer wants to create another order, code loops until input is "n"
    {
        string option;
        while (true) //asks the customer what option they want
        {
            try
            {
                Console.Write("Choose the Ice Cream option [Cup/Cone/Waffle]: ");
                option = Console.ReadLine().ToLower();
                if (option == "cup" || option == "cone" || option == "waffle") //if option is none of these, throws an exception until a valid input occurs
                {
                    break;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid input.");
            }
        }

        bool dipped = false;
        if (option == "cone") //checks if customer wants chocolate dipped
        {
            while (true)
            {
                try
                {
                    Console.Write("Do you want chocolate dip [Y/N]: ");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "y")
                    {
                        dipped = true;
                    }
                    else if (choice == "n")
                    {
                        dipped = false;
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }

            }

        }
        string waf = "";
        if (option == "waffle") //choose one of the 4 flavours as the price differs
        {
            while (true)
            {
                try
                {
                    Console.Write("Choose waffle flavour [Original/Red Velvet/Charcoal/Pandan]: ");
                    waf = Console.ReadLine().ToLower();
                    if (waf == "original" || waf == "red velvet" || waf == "charcoal" || waf == "pandan") //if input is none of these, throw an exception
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }
        int scoops;
        while (true) //specify number of scoops wanted, using int scoops in future parts
        {
            try
            {
                Console.Write("Input the number of scoops [1/2/3]: ");
                scoops = Convert.ToInt32(Console.ReadLine());
                if (scoops >= 1 && scoops <= 3)
                {
                    break;
                }
                else
                {
                    throw new Exception(); //if input is not between 1-3 an error will be thrown as it is over the scoop limit
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid input.");
            }
        }

        string flav;
        List<Flavour> flavours = new List<Flavour>();
        string fl = "";
        string[] fla;
        if (scoops > 1) //when scoops is more than 1, it runs this option 
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                    Console.Write("Choose the flavours of the scoops (separated by ','): ");
                    fl = Console.ReadLine().ToLower();
                    if (fl.Contains(","))
                    {
                        fla = fl.Split(',');
                        List<string> list = fla.ToList(); //changes the array of flavours into a list so that i can remove excess values (not the most efficient way)
                        for (int i = 0; i < fla.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(fla[i]))
                            {
                                throw new Exception();
                            }
                        }
                        if (list.Count > scoops)
                        {
                            int remove = list.Count - scoops;
                            list.RemoveRange(scoops, remove); //when the value is out of range of scoops, it removes from the specified value onwards
                        }
                        foreach (string f in list) //creates the flavour object defining if its true or false for each flavour
                        {
                            bool b = Prem(f);
                            flavours.Add(new Flavour(f, b)); //adds to the flavour list
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }

        }
        else
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                    Console.Write("Choose the flavour of the scoop: ");
                    flav = Console.ReadLine().ToLower();
                    if (flav == "vanilla" || flav == "chocolate" || flav == "strawberry" || flav == "durian" || flav == "ube" || flav == "sea salt") //if input is none of these, throw an exception
                    {
                        bool b = Prem(flav);
                        flavours.Add(new Flavour(flav, b));
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }

        }

        List<Topping> toppings = new List<Topping>(); //creates new topping list
        int tottop = 0;
        string[] tops;
        while (true)
        {
            try
            {
                Console.Write("Please enter number of toppings to add [1/2/3/4]: ");
                tottop = Convert.ToInt32(Console.ReadLine());
                if (tottop >= 0 && tottop <= 4)
                {
                    break;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch
            {
                Console.WriteLine("Please enter a valid input.");
            }
        }

        if (tottop > 1)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of toppings: Sprinkles, Mochi, Sago, Oreos");
                    Console.Write("Input the toppings to add (separated by ','): ");
                    string top = Console.ReadLine();
                    if (top.Contains(","))
                    {
                        tops = top.Split(",");
                        if (tops.Contains(" "))
                        {
                            throw new Exception();
                        }
                        List<string> list = tops.ToList(); //changes the array of topping into a list so that i can remove excess values (not the most efficient way)
                        for (int i = 0; i < tops.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(tops[i]))
                            {
                                throw new Exception();
                            }
                        }
                        if (tops.Length > tottop)
                        {
                            int remove = list.Count - tottop;
                            list.RemoveRange(tottop, remove); //when the value is out of range of toppings, it removes from the specified value onwards

                        }
                        foreach (string t in list)
                        {
                            toppings.Add(new Topping(t));
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }

        Order order = new Order();
        order.IceCreamList = new List<IceCream>();
        //creating IceCream objects and adding to the order, based on previous input
        if (option == "cup")
        {
            IceCream cup = new Cup(option, scoops, flavours, toppings);
            order.IceCreamList.Add(cup);
        }
        else if (option == "cone")
        {
            IceCream cone = new Cone(option, scoops, flavours, toppings, dipped);
            order.IceCreamList.Add(cone);
        }
        else if (option == "waffle")
        {
            IceCream waffle = new Waffle(option, scoops, flavours, toppings, waf);
            order.IceCreamList.Add(waffle);
        }

        if (customer.Rewards.Tier == "Gold") //if membership status is gold add to gold queue
        {
            goldQueue.Enqueue(order);
            Console.WriteLine("Order has been made successfully in gold queue.");
        }
        else //add all other membership status to normal queue
        {
            normalQueue.Enqueue(order);
            Console.WriteLine("Order has been made successfully in normal queue.");
        }
        Console.Write("Would you like to add another Ice Cream to the order [Y/N]: ");
        another = Console.ReadLine().ToLower();
    }
}*/

Customer cust = null;
IceCream icecream = null;
int orderid = 5;
//Basic Feature 4 (Jodie bcs i cldnt figure out how aydens code worked)
void option4()
{
    option1();
    int id;
    string ident;
    while (true) //ensures the customer ID entered is a valid input
    {
        try
        {
            Console.Write("Please enter customer ID: ");
            id = Convert.ToInt32(Console.ReadLine());
            bool test = false;
            foreach (Customer c in customerhist)
            {
                if (id == c.MemberId)
                {
                    test = true;
                    break;
                }
            }
            if (test == true)
            {
                break;
            }
            else
            {
                Console.WriteLine("Please enter a Member ID from the provided list.");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid input.");
        }
    }
    Dictionary<int, Customer> customers = custdict();
    foreach (Customer cc in customers.Values)
    {
        //Checks if customer exsists using customer dictionary
        if (cc.MemberId == id)
        {
            cust = cc;
            break;
        }
    } 
    foreach (Customer c4 in customerhist)
    {
        foreach (Order order4 in totalQueue)
        {
            if (order4 == c4.CurrentOrder)
            {
                orderid++;
            }
        }
    }
    Order neworder = new Order(orderid, DateTime.Now);
    bool another = true;
    int countOfIceCreams = 0;
    while (another != false) //if customer wants to create another order, code loops until input is "n"
    {
        string option;
        while (true) //asks the customer what option they want
        {
            try
            {
                Console.Write("Choose the Ice Cream option [Cup/Cone/Waffle]: ");
                option = Console.ReadLine().ToLower();
                if (option == "cup" || option == "cone" || option == "waffle") //if option is none of these, throws an exception until a valid input occurs
                {
                    break;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Please enter an input from the provided list.");
            }
        }

        bool dipped = false;
        if (option == "cone") //checks if customer wants chocolate dipped
        {
            while (true)
            {
                try
                {
                    Console.Write("Do you want chocolate dip [Y/N]: ");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "y")
                    {
                        dipped = true;
                        break;
                    }
                    else if (choice == "n")
                    {
                        dipped = false;
                        break;
                    }               
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }

            }

        }
        string waf = "";
        if (option == "waffle") //choose one of the 4 flavours as the price differs
        {
            while (true)
            {
                try
                {
                    Console.Write("Choose waffle flavour [Original/Red Velvet/Charcoal/Pandan]: ");
                    waf = Console.ReadLine().ToLower();
                    if (waf == "original" || waf == "red velvet" || waf == "charcoal" || waf == "pandan") //if input is none of these, throw an exception
                    {
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }
        int scoops = 0;
        while (true) //specify number of scoops wanted, using int scoops in future parts
        {
            try
            {
                Console.Write("Input the number of scoops [1/2/3]: ");
                scoops = Convert.ToInt32(Console.ReadLine());
                if (scoops >= 1 && scoops <= 3)
                {
                    break;
                }
                else
                {
                    throw new Exception(); //if input is not between 1-3 an error will be thrown as it is over the scoop limit
                }
            }
            catch
            {
                Console.WriteLine("Please enter an input in the provided range.");
            }
        }
        string flav;
        List<Flavour> flavours = new List<Flavour>();
        string fl = "";
        string[] fla;
        if (scoops > 1) //when scoops is more than 1, it runs this option 
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                    Console.Write("Choose the flavours of the scoops (separated by ','): ");
                    fl = Console.ReadLine().ToLower();
                    if (fl.Contains(","))
                    {
                        fla = fl.Split(',');
                        List<string> list = fla.ToList(); //changes the array of flavours into a list so that i can remove excess values (not the most efficient way)
                        for (int i = 0; i < fla.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(fla[i]))
                            {
                                throw new Exception();
                            }
                        }
                        if (list.Count > scoops)
                        {
                            int remove = list.Count - scoops;
                            list.RemoveRange(scoops, remove); //when the value is out of range of scoops, it removes from the specified value onwards
                        }
                        foreach (string f in list) //creates the flavour object defining if its true or false for each flavour
                        {
                            bool b = Prem(f);
                            flavours.Add(new Flavour(f, b)); //adds to the flavour list
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }
        else
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                    Console.Write("Choose the flavour of the scoop: ");
                    flav = Console.ReadLine().ToLower();
                    if (flav == "vanilla" || flav == "chocolate" || flav == "strawberry" || flav == "durian" || flav == "ube" || flav == "sea salt") //if input is none of these, throw an exception
                    {
                        bool b = Prem(flav);
                        flavours.Add(new Flavour(flav, b));
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }

                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }

        }

        List<Topping> toppings = new List<Topping>(); //creates new topping list
        int tottop = 0;
        string[] tops;
        while (true)
        {
            try
            {
                Console.Write("Please enter number of toppings to add [1/2/3/4]: ");
                tottop = Convert.ToInt32(Console.ReadLine());
                if (tottop >= 0 && tottop <= 4)
                {
                    break;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch
            {
                Console.WriteLine("Please enter a valid input.");
            }
        }

        if (tottop > 1)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of toppings: Sprinkles, Mochi, Sago, Oreos");
                    Console.Write("Input the toppings to add (separated by ','): ");
                    string top = Console.ReadLine();
                    if (top.Contains(","))
                    {
                        tops = top.Split(",");
                        List<string> list = tops.ToList(); //changes the array of topping into a list so that i can remove excess values (not the most efficient way)
                        for (int i = 0; i < tops.Length; i++)
                        {
                            if (string.IsNullOrWhiteSpace(tops[i]))
                            {
                                throw new Exception();
                            }
                        }
                        if (tops.Length > tottop)
                        {
                            int remove = list.Count - tottop;
                            list.RemoveRange(tottop, remove); //when the value is out of range of toppings, it removes from the specified value onwards
                        }
                        foreach (string t in list)
                        {
                            toppings.Add(new Topping(t));
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }
        }
        else
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Choices of toppings: Sprinkles, Mochi, Sago, Oreos");
                    Console.Write("Input the topping to add: ");
                    string top = Console.ReadLine().ToLower();
                    if (top == "sprinkles" || top == "mochi" || top == "sago" || top == "oreos") //if input is none of these, throw an exception
                    {
                        toppings.Add(new Topping(top));
                        break;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Please enter a valid input.");
                }
            }

        }
        if (option == "cup")
        {
            icecream = new Cup(option, scoops, flavours, toppings);
        }
        else if (option == "cone")
        {
            icecream = new Cone(option, scoops, flavours, toppings, dipped);
        }
        else if (option == "waffle")
        {
            icecream = new Waffle(option, scoops, flavours, toppings, waf);
        }
        if (countOfIceCreams == 0)
        {
            //creating IceCream objects and adding to the order, based on previous input
            neworder.IceCreamList = new List<IceCream>();
            neworder.IceCreamList.Add(icecream);
            countOfIceCreams++;
        }
        else
        {
            neworder.IceCreamList.Add(icecream);
        }
        string x;
        while (true)
        {
            Console.Write("Would you like to add another Ice Cream to the order [Y/N]: ");
            x = Console.ReadLine().ToLower();
            if (x != "n" && x != "y")
            {
                Console.WriteLine("Please give an input from the provided values.");
            }
            else if (x == "n")
            {
                another = false;
                break;
            }
            else if (x == "y")
            {
                break;
            }
        }
        if (x  == "n")
        {
            break;
        }
    }
    foreach (Customer cc in customerhist)
    {
        //Checks if customer exsists using customer dictionary
        if (cc.MemberId == id)
        {
            cc.CurrentOrder = neworder;
        }
    }
    totalQueue.Enqueue(neworder);
    if (cust.Rewards.Tier == "Gold") //if membership status is gold add to gold queue
    {
        goldQueue.Enqueue(neworder);
        Console.WriteLine("Order has been made successfully in gold queue.");
    }
    else //add all other membership status to normal queue
    {
        normalQueue.Enqueue(neworder);
        Console.WriteLine("Order has been made successfully in normal queue.");
    }
}

//Basic Feature 5
void option5()
{
    List<string> namelist = new List<string>();
    Console.WriteLine("Name List: ");
    foreach (Customer cus in customerhist)
    {
        Console.WriteLine(cus.Name);
        namelist.Add((cus.Name).ToLower());
    }
    while (true)
    {
        try
        {
            Console.Write("Enter a name from the list: ");
            string name = Console.ReadLine().ToLower();
            Console.WriteLine();
            if (namelist.Contains(name) == true)
            {
                foreach (Customer cus in customerhist)
                {
                    if (cus.Name.ToLower() == name)
                    {
                        Console.WriteLine("Current Order:");
                        if (cus.CurrentOrder != null)
                        {
                            Order currorder = cus.CurrentOrder;
                            Console.WriteLine(currorder.ToString());
                            foreach (IceCream icr in currorder.IceCreamList)
                            {
                                Console.WriteLine(icr);
                            }
                        }
                        else
                        {
                            Console.WriteLine("There is no current order for this customer.");
                        }
                        Console.WriteLine();
                        Console.WriteLine("Order History: ");
                        List<Order> ol = cus.OrderHistory;
                        foreach (Order o in ol)
                        {
                            Console.WriteLine(o.ToString());
                            foreach (IceCream ice in o.IceCreamList)
                            {
                                Console.WriteLine(ice);
                                Console.WriteLine();
                            }
                        }
                        if (ol.Count == 0)
                        {
                            Console.WriteLine("This customer has no logged order history.");
                        }
                        break;
                    }
                }
                break;
            }
            else
            {
                Console.WriteLine("Please enter a name from the provided list");
                continue;
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a name from the provided list");
            continue;
        }
    }
}
void option6()
{
    foreach (Customer cus in customerhist)
    {
        Console.WriteLine(cus.Name);
    }
    Customer c6 = null;
    while (true)
    {
        try
        {
            Console.Write("Enter a name from the list (Upper Case): ");
            string input = Console.ReadLine();
            bool check = false;
            foreach (Customer customer6 in customerhist)
            {
                if (input == customer6.Name)
                {
                    check = true;
                    c6 = customer6;
                    break;
                }
            }
            if (check == true)
            {
                break;
            }
        }
        catch
        {
            Console.WriteLine("Enter a name from the namelist");
        }
    }
    //Displays only the current orders
    Console.WriteLine("Current order:");
    Order order6 = null; //tracks order obj to print later
  
    if (c6.CurrentOrder != null)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(c6.CurrentOrder.ToString());
                Console.Write("1) Choose an existing ice cream to modify" +
                    "\n2) Add a new ice cream to the order" + "\n3) Delete an object from the order" + "\n4) Exit" +
                    "\nEnter an option: "); //creates option list
                int prompt = Convert.ToInt32(Console.ReadLine());
                if (prompt >= 1 && prompt <=3) //makes sure input can only be 1/2/3
                {
                    int count = 0;
                    foreach (Customer cus in customerhist) 
                    {
                        if (cus.MemberId == c6.MemberId)
                        {                           
                            foreach (IceCream ic in cus.CurrentOrder.IceCreamList) //loops through the icr list to print all ics in currorder
                            {
                                count++;
                                Console.WriteLine("{0, -5}{1}", (count + ") "), ic.ToString());
                            }
                            if (prompt == 1) // modify order
                            {
                                cus.CurrentOrder.ModifyIceCream(cus.CurrentOrder.Id);
                            }
                            if (prompt == 2)
                            {
                                string option;
                                while (true) //asks the customer what option they want
                                {
                                    try
                                    {
                                        Console.Write("Choose the Ice Cream option [Cup/Cone/Waffle]: ");
                                        option = Console.ReadLine().ToLower();
                                        if (option == "cup" || option == "cone" || option == "waffle") //if option is none of these, throws an exception until a valid input occurs
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            throw new Exception();
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter an input from the provided list.");
                                    }
                                }

                                bool dipped = false;
                                if (option == "cone") //checks if customer wants chocolate dipped
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("Do you want chocolate dip [Y/N]: ");
                                            string choice = Console.ReadLine().ToLower();
                                            if (choice == "y")
                                            {
                                                dipped = true;
                                            }
                                            else if (choice == "n")
                                            {
                                                dipped = false;
                                            }
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }

                                    }

                                }
                                string waf = "";
                                if (option == "waffle") //choose one of the 4 flavours as the price differs
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.Write("Choose waffle flavour [Original/Red Velvet/Charcoal/Pandan]: ");
                                            waf = Console.ReadLine().ToLower();
                                            if (waf == "original" || waf == "red velvet" || waf == "charcoal" || waf == "pandan") //if input is none of these, throw an exception
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }
                                    }
                                }
                                int scoops = 0;
                                while (true) //specify number of scoops wanted, using int scoops in future parts
                                {
                                    try
                                    {
                                        Console.Write("Input the number of scoops [1/2/3]: ");
                                        scoops = Convert.ToInt32(Console.ReadLine());
                                        if (scoops >= 1 && scoops <= 3)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            throw new Exception(); //if input is not between 1-3 an error will be thrown as it is over the scoop limit
                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter an input in the provided range.");
                                    }
                                }
                                string flav;
                                List<Flavour> flavours = new List<Flavour>();
                                string fl = "";
                                string[] fla;
                                if (scoops > 1) //when scoops is more than 1, it runs this option 
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                                            Console.Write("Choose the flavours of the scoops (separated by ','): ");
                                            fl = Console.ReadLine().ToLower();
                                            if (fl.Contains(","))
                                            {
                                                fla = fl.Split(',');
                                                List<string> list = fla.ToList(); //changes the array of flavours into a list so that i can remove excess values (not the most efficient way)
                                                if (list.Count > scoops)
                                                {
                                                    int remove = list.Count - scoops;
                                                    list.RemoveRange(scoops, remove); //when the value is out of range of scoops, it removes from the specified value onwards
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }
                                    }
                                    foreach (string f in fla) //creates the flavour object defining if its true or false for each flavour
                                    {
                                        bool b = Prem(f);
                                        flavours.Add(new Flavour(f, b)); //adds to the flavour list
                                    }
                                }
                                else
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choices of flavours are: Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt");
                                            Console.Write("Choose the flavour of the scoop: ");
                                            flav = Console.ReadLine().ToLower();
                                            if (flav == "vanilla" || flav == "chocolate" || flav == "strawberry" || flav == "durian" || flav == "ube" || flav == "sea salt") //if input is none of these, throw an exception
                                            {
                                                bool b = Prem(flav);
                                                flavours.Add(new Flavour(flav, b));
                                                break;
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }

                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }
                                    }

                                }

                                List<Topping> toppings = new List<Topping>(); //creates new topping list
                                int tottop = 0;
                                string[] tops;
                                while (true)
                                {
                                    try
                                    {
                                        Console.Write("Please enter number of toppings to add [1/2/3/4]: ");
                                        tottop = Convert.ToInt32(Console.ReadLine());
                                        if (tottop >= 0 && tottop <= 4)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            throw new Exception();
                                        }

                                    }
                                    catch
                                    {
                                        Console.WriteLine("Please enter a valid input.");
                                    }
                                }

                                if (tottop > 1)
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choices of toppings: Sprinkles, Mochi, Sago, Oreos");
                                            Console.Write("Input the toppings to add (separated by ','): ");
                                            string top = Console.ReadLine();
                                            if (top.Contains(","))
                                            {
                                                tops = top.Split(",");
                                                List<string> list = tops.ToList(); //changes the array of topping into a list so that i can remove excess values (not the most efficient way)
                                                if (tops.Length > tottop)
                                                {
                                                    int remove = list.Count - tottop;
                                                    list.RemoveRange(tottop, remove); //when the value is out of range of toppings, it removes from the specified value onwards
                                                }
                                                foreach (string t in tops)
                                                {
                                                    toppings.Add(new Topping(t));
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                            break;
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }
                                    }
                                }
                                else
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("Choices of toppings: Sprinkles, Mochi, Sago, Oreos");
                                            Console.Write("Input the topping to add: ");
                                            string top = Console.ReadLine().ToLower();
                                            if (top == "sprinkles" || top == "mochi" || top == "sago" || top == "oreos") //if input is none of these, throw an exception
                                            {
                                                toppings.Add(new Topping(top));
                                                break;
                                            }
                                            else
                                            {
                                                throw new Exception();
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Please enter a valid input.");
                                        }
                                    }

                                }
                                if (option == "cup")
                                {
                                    icecream = new Cup(option, scoops, flavours, toppings);
                                }
                                else if (option == "cone")
                                {
                                    icecream = new Cone(option, scoops, flavours, toppings, dipped);
                                }
                                else if (option == "waffle")
                                {
                                    icecream = new Waffle(option, scoops, flavours, toppings, waf);
                                }
                                cus.CurrentOrder.AddIceCream(icecream);
                            }
                            if (prompt == 3)
                            {
                                while (true)
                                {
                                    int cou = 0;
                                    foreach (IceCream ic in cus.CurrentOrder.IceCreamList)
                                    {
                                        cou++;
                                    }
                                    Console.Write("Enter the number of which's ice cream you want to delete: ");
                                    int c = Convert.ToInt32(Console.ReadLine());
                                    if (c == 1)
                                    {
                                        Console.WriteLine("You cannot delete this ice cream as an order cannot have 0 ice creams.");
                                    }
                                    else if (c > 1 && c <= cou)
                                    {
                                        cus.CurrentOrder.DeleteIceCream(c);
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enter a value from the range provided.");
                                    }
                                }
                            }
                        }
                        order6 = cus.CurrentOrder;
                    } 
                }
                else if (prompt == 4) //exits the loop without reprinting the ice cream list
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a value from the provided range.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number from the given list.");
            }
        }
        Console.WriteLine("Newly updated order: ");
        Console.WriteLine(order6.ToString());
        foreach (IceCream icr in order6.IceCreamList)
        {
            Console.WriteLine(icr.ToString());
            Console.WriteLine();
        }
    }
    else
    {
        Console.WriteLine("This customer has no current order."); //shows that there is no current order to edit/add to/delete from
    }
}

void advA() //Ayden
{
    Order or = new Order();
    if (goldQueue.Count > 0)
    {
        or = goldQueue.Dequeue();
        Console.WriteLine("Starting first order for gold queue.");
    }
    else if (normalQueue.Count > 0)
    {
        or = normalQueue.Dequeue();
        Console.WriteLine("Starting first order for normal queue.");
    }
    else
    {
        Console.WriteLine("There are no current orders.");
        return;
    }
    /*Dictionary<int, Customer> customers = custdict();
    Customer customer = customers[or.Id];
    Console.WriteLine($"Customer name: {customer.Name}");
    double bill = or.CalculateTotal();
    Console.WriteLine($"Total bill: {bill}");

    foreach(IceCream ice in or.IceCreamList)
    {
        Console.WriteLine(ice.ToString());
    }*/
}

void advB() //Jodie
{
    int year;
    while (true)
    {
        try
        {
            Console.Write("Enter the year: ");
            year = Convert.ToInt32(Console.ReadLine());
            if (year > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Please enter a valid year.");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please enter a valid input.");
        }
    }
    Console.WriteLine();
    double total = 0;
    foreach (Order o in totalQueue)
    {
        if (o.IceCreamList != null)
        {
            if (Convert.ToDateTime(o.TimeFulfilled).Year == year)
            {
                double price = 0;
                foreach (IceCream icr in o.IceCreamList)
                {
                    price += icr.CalculatePrice();
                }
                if (price > 0)
                {
                    total += price;
                    Console.WriteLine("{0,-10}${1,-7}", (Convert.ToDateTime(o.TimeFulfilled).ToString("MMM") + " " + Convert.ToDateTime(o.TimeFulfilled).Year), Math.Round(price));
                }
            }
        }
    }
    if (total == 0)
    {
        Console.WriteLine("There were no orders fufilled in the specified year.");
    }
    else
    {
        Console.WriteLine("\nTotal:    $" + Math.Round(total));
    }
}
//create menu
customerList();
pastOrders();
while (true)
{
    try
    {
        Console.WriteLine("----------------M E N U --------------------");
        Console.WriteLine("[1] List all customers");
        Console.WriteLine("[2] List all current orders");
        Console.WriteLine("[3] Register a new customer");
        Console.WriteLine("[4] Create a customer's order");
        Console.WriteLine("[5] Display order details of a customer");
        Console.WriteLine("[6] Modify order details");
        Console.WriteLine("[7] Process an order and checkout");
        Console.WriteLine("[8] Display monthly charged amounts breakdown & total charged amounts for the year");
        Console.WriteLine("[0] Exit");
        Console.WriteLine("---------------------------------------------");
        Console.Write("Enter your option : ");
        int opt = Convert.ToInt32(Console.ReadLine());
        if (opt > 8 || opt < 0)
        {
            Console.WriteLine("Please enter an option from the given range.");
            Console.WriteLine();
            continue;
        }
        else if (opt == 1)
        {
            option1();
            Console.WriteLine();
        }
        else if (opt == 2)
        {
            option2();
            Console.WriteLine();
        }
        else if (opt == 3)
        {
            option3();
            Console.WriteLine();
        }
        else if (opt == 4)
        {
            option4();
            Console.WriteLine();
        }
        else if (opt == 5)
        {
            option5();
            Console.WriteLine();
        }
        else if (opt == 6)
        {
            option6();
            Console.WriteLine();
        }
        else if (opt == 7)
        {
            advA();
            Console.WriteLine();
        }
        else if (opt == 8)
        {
            advB();
            Console.WriteLine();
        }
        else
        {
            break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Please enter an integer from 0-8");
        Console.WriteLine();
    } 
}