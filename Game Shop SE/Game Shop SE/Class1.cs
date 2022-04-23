using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Shop_SE
{
    class Toy
    {
        private string Name;
        private int UPC;
        private double Price;

        public Toy(string name, int uPC, double price)
        {
            Name = name;
            UPC = uPC;
            Price = Math.Round(price,2);
        }

        public string GetName()
        {
            return Name;
        }

        public int GetUPC()
        {
            return UPC;
        }

        public double GetPrice()
        {
            return Price;
        }

        public double CalculateTheRealPrice(double TaxModifier) {
            return Math.Round(TaxModifier / 100 * Price + Price, 2);
        }

        public void PrintToyPriceAndRealPrice(double TaxModifier) {
            Console.Write(Name," :Cena ", Price, " din pre poreza i ", CalculateTheRealPrice(TaxModifier), "din nakon ", TaxModifier, "% poreza");
            Console.Write(" :Cena ");
            Console.Write(Price);
            Console.Write(" din pre poreza i ");
            Console.Write(CalculateTheRealPrice(TaxModifier));
            Console.Write("din nakon ");
            Console.Write(TaxModifier);
            Console.Write("% poreza");
            Console.WriteLine();
        }

        public override bool Equals(object obj)
        {
            var toy = obj as Toy;
            return toy != null &&
                   Name == toy.Name &&
                   UPC == toy.UPC &&
                   Price == toy.Price;
        }

    }
    class ToyStore 
    {
        private string Name;
        private int Tax;
        private List<Toy> Toys;

        public ToyStore(string name, int tax = 20)
        {
            Name = name;
            Tax = tax;
            Toys = new List<Toy>();
        }

        public string GetName()
        {
            return Name;
        }

        public int GetTax()
        {
            return Tax;
        }

        public void SetTax(int TaxAmount) {
            Tax = TaxAmount;
        }

        public List<Toy> GetToys()
        {
            return Toys;
        }

        public void AddToy(string ToyName, int ToyUPC,double ToyPrice) {
            Toys.Add(new Toy(ToyName, ToyUPC, ToyPrice));
        }

        static public void Main(String[] args) {
            Console.WriteLine("Unesite ime prodavnice");
            string TempName = Console.ReadLine();
            ToyStore Store = new ToyStore(TempName);
            bool TempFlag = true;
            string TempString = "";
            int TempInt;
            while (TempFlag) {
                Console.WriteLine("Unesite broj za neku od narednih komandi");
                Console.WriteLine("Naredne Komande su");
                Console.WriteLine("1. Dodajte igracku u prodavnicu");
                Console.WriteLine("2. Promenite obracunavajuci porez");
                Console.WriteLine("3. Ispisite cene proizvoda");
                Console.WriteLine("4. Kraj izvrsavanja programa");
                TempString = Console.ReadLine();
                TempInt = Convert.ToInt16(TempString);
                if (TempInt <= 0 || TempInt > 4) {
                    TempInt = 0;
                }

                switch (TempInt) {
                    case 0:
                        Console.WriteLine("Nekoreektan unos");
                        break;
                    case 1:
                        bool Case1FlagUPC = true;
                        bool Case1FlagPrice = true;
                        Console.WriteLine("Unesite ime igracake");
                        TempName = Console.ReadLine();
                        int TempUPC=0;
                        while (Case1FlagUPC)
                        {
                            Console.WriteLine("Unesite bar kod igracake");
                            TempString = Console.ReadLine();
                            TempUPC = Convert.ToInt32(TempString);
                            if (TempUPC < 0)
                            {
                                Console.WriteLine("Nekortektan unos");
                            }
                            else {
                                Case1FlagUPC = false;
                            }
                        }
                        double TempPrice=0;
                        while (Case1FlagPrice)
                        {
                            Console.WriteLine("Unesite cenu igracake");
                            TempString = Console.ReadLine();
                            TempPrice = Convert.ToDouble(TempString);
                            if (TempPrice < 0)
                            {
                                Console.WriteLine("Nekortektan unos");
                            }
                            else
                            {
                                Case1FlagPrice = false;
                            }
                        }
                        Store.AddToy(TempName, TempUPC, TempPrice);
                        break;
                    case 2:
                        bool Case2Flag = true;
                        int TempTax = 0;
                        while (Case2Flag)
                        {
                            Console.WriteLine("Unesite porez");
                            TempString = Console.ReadLine();
                            TempTax = Convert.ToInt32(TempString);
                            if (TempTax < 0)
                            {
                                Console.WriteLine("Nekortektan unos");
                            }
                            else
                            {
                                Case2Flag = false;
                            }
                        }
                        Store.SetTax(TempTax);
                        break;
                    case 3:
                        foreach (Toy temp in Store.GetToys()) {
                            temp.PrintToyPriceAndRealPrice(Store.GetTax());
                        }
                        break;
                    case 4:
                        TempFlag = false;
                        break;
                }
            }
        
        }
    }
}
