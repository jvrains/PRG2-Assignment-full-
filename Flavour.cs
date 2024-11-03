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
    internal class Flavour
    {
        private string type;
        private bool premium;

        public string Type { get; set; }
        public bool Premium { get; set; }
        
        public Flavour() { }
        public Flavour(string type, bool premium)
        {
            Type = type;
            Premium = premium;
        }
        public override string ToString()
        {
            return $"Flavour: {Type} \tPremium: {Premium}";
        }
    }
}
