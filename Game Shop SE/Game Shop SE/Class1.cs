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

        public void PrintToyPriceAndRealPrice(double TaxModifier,double GlobalDiscoount,List<int> UPCSpecificDiscountList, List<double> SpecificDiscountAmount) {
            int i = 0;
            double SpecificDiscount = 0;
            foreach (int iter in UPCSpecificDiscountList) {
                if (UPC == iter) {
                    break;
                }
                i++;
            }
            if (i < SpecificDiscountAmount.Count) {
                SpecificDiscount = SpecificDiscountAmount[i];
            }
            double FullDiscount = 0;
            if (GlobalDiscoount + SpecificDiscount > 100) { FullDiscount = 100; }
            else { FullDiscount = GlobalDiscoount + SpecificDiscount; }

            Console.Write(Name," :Cena ", Price, " din pre poreza i ", CalculateTheRealPrice(TaxModifier), "din nakon ", TaxModifier, "% poreza");
            Console.Write(" :Cena ");
            Console.Write(Price);
            Console.Write(" din pre poreza i ");
            Console.Write(CalculateTheRealPrice(TaxModifier));
            Console.Write(" din nakon ");
            Console.Write(TaxModifier);
            Console.Write("% poreza");
            if (FullDiscount != 0) {
                double discountAmount = Math.Round(CalculateTheRealPrice(TaxModifier) * FullDiscount / 100, 2);
                double RealPrice = CalculateTheRealPrice(TaxModifier) - discountAmount;
                string RealPriceString = RealPrice.ToString();
                Console.Write(" i ");
                Console.Write(RealPriceString, " nakon obracunatog popusta od ", FullDiscount,"%");
                Console.Write(" nakon obracunatog popusta od ");
                Console.Write(FullDiscount);
                Console.Write("%");
                Console.WriteLine();
                Console.Write("Ustedjen novac je ");
                Console.Write(discountAmount);
                Console.Write(" din");
            }
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
        private double GlobalDiscount;
        private List<int> ToysUPCWithBonusDiscount;
        private List<double> ToysWithBonusDiscountAmount;

        public ToyStore(string name)
        {
            Name = name;
            Tax = 20;
            Toys = new List<Toy>();
            GlobalDiscount = 0;
            ToysUPCWithBonusDiscount = new List<int>();
            ToysWithBonusDiscountAmount = new List<double>();
        }

        public string GetName()
        {
            return Name;
        }

        public double GetTax()
        {
            return Tax;
        }

        public double GetGlobalDiscount()
        {
            return GlobalDiscount;
        }

        public List<Toy> GetToys()
        {
            return Toys;
        }

        public List<int> GetToysUPCWithBonusDiscount()
        {
            return ToysUPCWithBonusDiscount;
        }

        public List<double> GetToysWithBonusDiscountAmount()
        {
            return ToysWithBonusDiscountAmount;
        }

        public void SetTax(double TaxAmount) {
            Math.Round(TaxAmount, 2);
            Tax = TaxAmount;
        }

        public void SetGlobalDiscount(double DiscountAmount)
        {
            Math.Round(DiscountAmount, 2);
            GlobalDiscount = DiscountAmount;
        }

        public void AddToy(string ToyName, int ToyUPC,double ToyPrice) {
            Toys.Add(new Toy(ToyName, ToyUPC, ToyPrice));
        }

        public void PrintToys() {
            foreach (Toy temp in Toys)
            {
                temp.PrintToyPriceAndRealPrice(Tax,GlobalDiscount,ToysUPCWithBonusDiscount,ToysWithBonusDiscountAmount);
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
            //
            Console.WriteLine("Unesite porez za obracunavanmje igracaka");
            Console.WriteLine("Porez mora biti pozitivna realna vrednost");
            flag = true;
            TempName = null;
            double TempTax = 20;
            while (flag)
            {
                TempName = Console.ReadLine();
                bool doubleChecker = double.TryParse(TempName, out TempTax);
                if (doubleChecker & TempTax>=0)
                {
                    flag = false;
                }
                else if (TempName.EndsWith("%")) {
                    TempName = TempName.Remove(TempName.Length - 1);
                    doubleChecker = double.TryParse(TempName, out TempTax);
                    if (doubleChecker & TempTax>=0)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                    Console.WriteLine("Porez mora biti pozitivan realan broj");
                }
            }
            Store.SetTax(TempTax);
            //
            Console.WriteLine("Unesite globalni popust koji se obracunava za sve igracke");
            Console.WriteLine("Popust mora biti pozitivna realna vrednost manja od 100");
            flag = true;
            TempName = null;
            double TempGlobalDiscount = 0;
            while (flag)
            {
                TempName = Console.ReadLine();
                bool doubleChecker = double.TryParse(TempName, out TempGlobalDiscount);
                if (doubleChecker & TempGlobalDiscount >= 0 & TempGlobalDiscount <= 100)
                {
                    flag = false;
                }
                else if (TempName.EndsWith("%"))
                {
                    TempName = TempName.Remove(TempName.Length - 1);
                    doubleChecker = double.TryParse(TempName, out TempGlobalDiscount);
                    if (doubleChecker & TempGlobalDiscount >= 0 & TempGlobalDiscount <= 100)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                    Console.WriteLine("Popust mora biti pozitivan realan broj manji od 100");
                }
            }
            Store.SetGlobalDiscount(TempGlobalDiscount);
            //
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
                    Console.WriteLine("Bar kod mora biti pozitivan ceo broj");
                    TempName = Console.ReadLine();
                    bool intChecker = int.TryParse(TempName, out TempToyUpc);
                    if (intChecker & TempToyUpc>=0)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Bar kod mora biti pozitivan ceo broj");
                    }
                }
                flag = true;
                while (flag)
                {
                    Console.WriteLine("Cena:");
                    Console.WriteLine("Cena mora biti pozitivan realan broj");
                    TempName = Console.ReadLine();
                    bool doubleChecker = double.TryParse(TempName, out TempToyPrice);
                    if (doubleChecker & TempToyPrice>=0)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Cena mora biti pozitivan realan broj");
                    }
                }
                Store.AddToy(TempToyName, TempToyUpc, TempToyPrice);
                Console.WriteLine("Unesite sledecu igracku ili unosom imena Stop zaustavite unos");
                flag = true;
            }
            //
            Console.WriteLine("Unesite igracke na koje ce se obracunavati dodatni popust tako sto cete odabrati redni broj igracke a zatim iznos dodatnog popusta");
            Console.WriteLine("Ukoliko zelite da obustavite unos igracaka sa popustom unesite broj 0 za redni broj");
            int iter = 1;
            foreach (Toy TempToy in Store.GetToys()) {
                Console.Write(iter);
                Console.Write(". ");
                Console.Write(TempToy.GetName());
                Console.Write(" Bar kod: ");
                Console.Write(TempToy.GetUPC());
                Console.WriteLine();
                iter++;
            }
            flag = true;
            TempName = null;
            int TempToyDiscountUpc = 0;
            double TempToyDiscount = 0;
            while (true)
            {
                Console.WriteLine("Unos igracke");
                if (TempName == "Stop" | TempName == "stop") { break; }
                TempToyName = TempName;
                flag = true;
                while (flag)
                {
                    Console.WriteLine("Redni broj:");
                    Console.WriteLine("Redni broj mora biti neki od gorenavedenih igracaka");
                    TempName = Console.ReadLine();
                    bool intChecker = int.TryParse(TempName, out TempToyDiscountUpc);
                    if (intChecker & TempToyDiscountUpc >= 0 & TempToyDiscountUpc<iter)
                    {
                        flag = false;
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Redni broj mora biti neki od gorenavedenih igracaka");
                    }
                }
                if (TempToyDiscountUpc == 0) { break; }
                TempToyDiscountUpc = Store.GetToys()[TempToyDiscountUpc - 1].GetUPC();
                int index = -1;
                if (Store.GetToysUPCWithBonusDiscount().Contains(TempToyDiscountUpc))
                {
                    index = Store.GetToysUPCWithBonusDiscount().IndexOf(TempToyDiscountUpc);
                }
                else
                {
                    Store.GetToysUPCWithBonusDiscount().Add(TempToyDiscountUpc);
                }
                Console.WriteLine("Popust:");
                Console.WriteLine("Popust mora biti pozitivan realan broj manji od 100");
                flag = true;
                while (flag)
                {
                    TempName = Console.ReadLine();
                    bool doubleChecker = double.TryParse(TempName, out TempToyDiscount);
                    if (doubleChecker & TempToyDiscount >= 0 & TempToyDiscount <= 100)
                    {
                        flag = false;
                    }
                    else if (TempName.EndsWith("%"))
                    {
                        TempName = TempName.Remove(TempName.Length - 1);
                        doubleChecker = double.TryParse(TempName, out TempToyDiscount);
                        if (doubleChecker & TempToyDiscount >= 0 & TempToyDiscount <= 100)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        Console.WriteLine("Nekorektan unos, molim Vas unesite ponovo");
                        Console.WriteLine("Popust mora biti pozitivan realan broj manji od 100");
                    }
                }
                if (index != 0)
                {
                    Store.GetToysWithBonusDiscountAmount().Add(TempToyDiscount);
                }
                else {
                    Store.GetToysWithBonusDiscountAmount()[index] = TempToyDiscount;
                }
                Console.WriteLine("Unesite sledecu igracku ili unosom 0 zaustavite unos");
                flag = true;
            }
            //
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
