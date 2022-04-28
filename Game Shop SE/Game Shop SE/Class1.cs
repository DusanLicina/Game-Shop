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
            Console.Write(" din nakon ");
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
        private double Tax;
        private List<Toy> Toys;

        public ToyStore(string name)
        {
            Name = name;
            Tax = 20;
            Toys = new List<Toy>();
        }

        public string GetName()
        {
            return Name;
        }

        public double GetTax()
        {
            return Tax;
        }

        public void SetTax(double TaxAmount) {
            Math.Round(TaxAmount, 2);
            Tax = TaxAmount;
        }

        public List<Toy> GetToys()
        {
            return Toys;
        }

        public void AddToy(string ToyName, int ToyUPC,double ToyPrice) {
            Toys.Add(new Toy(ToyName, ToyUPC, ToyPrice));
        }

        public void PrintToys() {
            foreach (Toy temp in Toys)
            {
                temp.PrintToyPriceAndRealPrice(Tax);
            }
        }
            static public void Main(String[] args) {
            Console.WriteLine("Unesite ime prodavnice");
            bool flag = true;
            string TempName = null;
            while (flag)
            {
                TempName = Console.ReadLine();
                if (TempName != " " & TempName.Length <= 50)
                {
                    flag = false;
                }
                if (flag) {
                    Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                    Console.WriteLine("Ili ste uneli prazno ime ili ime ima previse karaktera");
                }
            }
            ToyStore Store = new ToyStore(TempName);
            Console.WriteLine("Unesite porez za obracunavanmje igracaka");
            Console.WriteLine("Porez mora biti realna vrednost");
            flag = true;
            TempName = null;
            double TempTax = 20;
            while (flag)
            {
                TempName = Console.ReadLine();
                bool doubleChecker = double.TryParse(TempName, out TempTax);
                if (doubleChecker)
                {
                    flag = false;
                }
                else if (TempName.EndsWith("%")) {
                    TempName = TempName.Remove(TempName.Length - 1);
                    doubleChecker = double.TryParse(TempName, out TempTax);
                    if (doubleChecker)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                    Console.WriteLine("Porez mora biti realan broj");
                }
            }
            Store.SetTax(TempTax);
            Console.WriteLine("Unesite igracke u prodavnicu tako sto cete uneti ime zatim bar kod i na kraju cenu igracke");
            Console.WriteLine("Ukoliko za ime unesete Stop prekinucete unos igracaka");
            flag = true;
            TempName = null;
            string TempToyName = null;
            int TempToyUpc = 0;
            double TempToyPrice = 0;
            while (true)
            {
                Console.WriteLine("Unos igracke");
                while (flag)
                {
                    Console.WriteLine("Ime:");
                    TempName = Console.ReadLine();
                    if (TempName != " " & TempName.Length <= 50)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Ili ste uneli prazno ime ili ime ima previse karaktera");
                    }
                }
                if (TempName == "Stop" | TempName == "stop") { break; }
                TempToyName = TempName;
                flag = true;
                while (flag)
                {
                    Console.WriteLine("Bar kod:");
                    Console.WriteLine("Bar kod mora biti ceo broj");
                    TempName = Console.ReadLine();
                    bool intChecker = int.TryParse(TempName, out TempToyUpc);
                    if (intChecker)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Bar kod mora biti ceo broj");
                    }
                }
                flag = true;
                while (flag)
                {
                    Console.WriteLine("Cena:");
                    Console.WriteLine("Cena mora biti realan broj");
                    TempName = Console.ReadLine();
                    bool doubleChecker = double.TryParse(TempName, out TempToyPrice);
                    if (doubleChecker)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Cena mora biti realan broj");
                    }
                }
                Store.AddToy(TempToyName, TempToyUpc, TempToyPrice);
                Console.WriteLine("Unesite sledecu igracku ili unosom imena Stop zaustavite unos");
                flag = true;
            }
            Store.PrintToys();
            TempName = Console.ReadLine();

            //bool TempFlag = true;
            //string TempString = "";
            //int TempInt;
            //while (TempFlag) {
            //    Console.WriteLine("Unesite broj za neku od narednih komandi");
            //    Console.WriteLine("Naredne Komande su");
            //    Console.WriteLine("1. Dodajte igracku u prodavnicu");
            //    Console.WriteLine("2. Promenite obracunavajuci porez");
            //    Console.WriteLine("3. Ispisite cene proizvoda");
            //    Console.WriteLine("4. Kraj izvrsavanja programa");
            //    TempString = Console.ReadLine();
            //    TempInt = Convert.ToInt16(TempString);
            //    if (TempInt <= 0 || TempInt > 4) {
            //        TempInt = 0;
            //    }

            //    switch (TempInt) {
            //        case 0:
            //            Console.WriteLine("Nekoreektan unos");
            //            break;
            //        case 1:
            //            bool Case1FlagUPC = true;
            //            bool Case1FlagPrice = true;
            //            Console.WriteLine("Unesite ime igracake");
            //            TempName = Console.ReadLine();
            //            int TempUPC=0;
            //            while (Case1FlagUPC)
            //            {
            //                Console.WriteLine("Unesite bar kod igracake");
            //                TempString = Console.ReadLine();
            //                TempUPC = Convert.ToInt32(TempString);
            //                if (TempUPC < 0)
            //                {
            //                    Console.WriteLine("Nekortektan unos");
            //                }
            //                else {
            //                    Case1FlagUPC = false;
            //                }
            //            }
            //            double TempPrice=0;
            //            while (Case1FlagPrice)
            //            {
            //                Console.WriteLine("Unesite cenu igracake");
            //                TempString = Console.ReadLine();
            //                TempPrice = Convert.ToDouble(TempString);
            //                if (TempPrice < 0)
            //                {
            //                    Console.WriteLine("Nekortektan unos");
            //                }
            //                else
            //                {
            //                    Case1FlagPrice = false;
            //                }
            //            }
            //            Store.AddToy(TempName, TempUPC, TempPrice);
            //            break;
            //        case 2:
            //            bool Case2Flag = true;
            //            int TempTax = 0;
            //            while (Case2Flag)
            //            {
            //                Console.WriteLine("Unesite porez");
            //                TempString = Console.ReadLine();
            //                TempTax = Convert.ToInt32(TempString);
            //                if (TempTax < 0)
            //                {
            //                    Console.WriteLine("Nekortektan unos");
            //                }
            //                else
            //                {
            //                    Case2Flag = false;
            //                }
            //            }
            //            Store.SetTax(TempTax);
            //            break;
            //        case 3:
            //            foreach (Toy temp in Store.GetToys()) {
            //                temp.PrintToyPriceAndRealPrice(Store.GetTax());
            //            }
            //            break;
            //        case 4:
            //            TempFlag = false;
            //            break;
            //    }
            //}

        }
    }
}
