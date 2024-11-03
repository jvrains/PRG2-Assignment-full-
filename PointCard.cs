// Student Number : S10257108
// Student Name : Jodie Yap
// Partner Name : Ayden See

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace S10257108_PRG2Assignment
{
    internal class PointCard
    {
        private int points;
        private int punchCard;
        private string tier;

        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        public PointCard() { }

        public PointCard(int p, int pc)
        {
            Points = p;
            PunchCard = pc;
            Tier = "Ordinary";
        }

        public void AddPoints(int newpoints)
        {
            Points += newpoints;
        }

        public void RedeemPoints(int redeemed)
        {
            Points -= redeemed;
        }

        public void Punch()
        {
            PunchCard ++;
            if (PunchCard > 10)
            {
                PunchCard = 0;
                bool free = true;
            }
        }

        public string ToString()
        {
            return $"Points: {Points}" +
                $"PunchCard: {PunchCard}" +
                $"Tier: {Tier}";
        }
    }
}
