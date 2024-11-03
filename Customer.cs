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
    internal class Customer
    {
        private string name;
        private int memberId;
        private DateTime dob;
        private Order currentOrder;
        private List<Order> orderHistory;
        private PointCard rewards;

        public string Name { get; set; }
        public int MemberId { get; set; }
        public DateTime Dob { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; }
        public PointCard Rewards { get; set; }

        public Customer() { }
        public Customer(string n, int mi, DateTime dob)
        {
            Name = n;
            MemberId = mi;
            Dob = dob;
        }
        public Order MakeOrder()
        {
            Order order = new Order();
            return order;
        }
        public bool IsBirthday()
        {
            if (CurrentOrder.TimeReceived.Date == Dob)
            {
                return true;
            }
            else { return false; }
        }
        public string ToString()
        {
            return $"Name: {Name}" +
              $"MemberID: {MemberId}" +
              $"Date of Birth: {Dob}";
        }

    }
}
