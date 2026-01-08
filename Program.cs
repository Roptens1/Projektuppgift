using System;
using System.Collections.Generic;

namespace Projektuppgift
{
    class Program
    {
        static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name = "Mjölk", Price = 15, Stock = 20 },
            new Product { Id = 2, Name = "Bröd", Price = 25, Stock = 10 },
            new Product { Id = 3, Name = "Ägg",  Price = 30, Stock = 50 }
        };

        static List<CartItem> cart = new List<CartItem>();

        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Console.Clear();
                Console.WriteLine("MENY");
                Console.WriteLine("1. Visa produkter");
                Console.WriteLine("2. Lägg till produkt i kundvagn");
                Console.WriteLine("3. Hantera kundvagn");
                Console.WriteLine("4. Admin – hantera produkter");
                Console.WriteLine("5. Avsluta & visa kvitto");
                Console.Write("Välj: ");

                string val = Console.ReadLine();

                switch (val)
                {
                    case "1": VisaProdukter(); break;
                    case "2": LäggTillProdukt(); break;
                    case "3": HanteraKundvagn(); break;
                    case "4": AdminMeny(); break;
                    case "5":
                        VisaKvitto();
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Fel val.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // ===== VISA PRODUKTER =====
        static void VisaProdukter()
        {
            Console.Clear();
            foreach (Product p in products)
                Console.WriteLine($"ID:{p.Id} | {p.Name} | {p.Price} kr | Lager:{p.Stock}");
            Console.ReadLine();
        }

        // ===== LÄGG TILL =====
        static void LäggTillProdukt()
        {
            try
            {
                VisaProdukter();

                Console.Write("Produkt-ID: ");
                int id = int.Parse(Console.ReadLine());

                Product p = products.Find(x => x.Id == id);
                if (p == null)
                {
                    Console.WriteLine("Produkten finns inte.");
                    Console.ReadLine();
                    return;
                }

                Console.Write("Antal: ");
                int qty = int.Parse(Console.ReadLine());

                if (qty <= 0 || qty > p.Stock)
                {
                    Console.WriteLine("Fel antal.");
                    Console.ReadLine();
                    return;
                }

                CartItem item = cart.Find(c => c.Product.Id == id);
                if (item == null)
                    cart.Add(new CartItem { Product = p, Quantity = qty });
                else
                    item.Quantity += qty;

                p.Stock -= qty;

                Console.WriteLine("Produkten har lagts till i kundvagnen.");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Fel inmatning. Använd siffror.");
                Console.ReadLine();
            }
        }


        // ===== HANTERA KUNDVAGN =====
        static void HanteraKundvagn()
        {
            Console.Clear();
            if (cart.Count == 0)
            {
                Console.WriteLine("Kundvagnen är tom.");
                Console.ReadLine();
                return;
            }

            double total = 0;
            foreach (CartItem c in cart)
            {
                double sum = c.Product.Price * c.Quantity;
                Console.WriteLine($"ID:{c.Product.Id} {c.Product.Name} x{c.Quantity} = {sum} kr");
                total += sum;
            }

            Console.WriteLine($"\nTotal: {total} kr");
            Console.WriteLine("1. Ändra antal");
            Console.WriteLine("2. Ta bort produkt");
            Console.WriteLine("3. Töm kundvagn");
            Console.WriteLine("4. Tillbaka");
            Console.Write("Välj: ");

            string val = Console.ReadLine();

            if (val == "1") ÄndraAntal();
            if (val == "2") TaBortProdukt();
            if (val == "3") TömKundvagn();
        }

        static void ÄndraAntal()
        {
            Console.Write("Produkt-ID: ");
            int id = int.Parse(Console.ReadLine());

            CartItem item = cart.Find(c => c.Product.Id == id);
            if (item == null) return;

            Console.Write("Nytt antal: ");
            int nytt = int.Parse(Console.ReadLine());

            int diff = nytt - item.Quantity;
            if (diff > item.Product.Stock) return;

            item.Product.Stock -= diff;
            item.Quantity = nytt;
        }

        static void TaBortProdukt()
        {
            Console.Write("Produkt-ID: ");
            int id = int.Parse(Console.ReadLine());

            CartItem item = cart.Find(c => c.Product.Id == id);
            if (item == null) return;

            item.Product.Stock += item.Quantity;
            cart.Remove(item);
        }

        static void TömKundvagn()
        {
            foreach (CartItem c in cart)
                c.Product.Stock += c.Quantity;
            cart.Clear();
            Console.WriteLine("Kundvagnen är tömd.");
            Console.ReadLine();
        }

        // ===== ADMIN =====
        static void AdminMeny()
        {
            Console.Clear();
            Console.Write("Lösenord: ");
            if (Console.ReadLine() != "admin")
            {
                Console.WriteLine("Fel lösenord.");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("ADMINMENY");
            Console.WriteLine("1. Lägg till produkt");
            Console.WriteLine("2. Ta bort produkt");
            Console.WriteLine("3. Ändra pris på produkt");
            Console.WriteLine("4. Ändra lagerantal");
            Console.WriteLine("5. Tillbaka");
            Console.Write("Välj: ");

            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    AdminLaggTillProdukt();
                    break;

                case "2":
                    AdminTaBortProdukt();
                    break;

                case "3":
                    AdminAndraPris();
                    break;

                case "4":
                    AdminAndraLager();
                    break;
            }
        }
        //ADMIN ÄNDRA PRIS
        static void AdminAndraPris()
        {
            VisaProdukter();

            Console.Write("Produkt-ID: ");
            int id = int.Parse(Console.ReadLine());

            Product p = products.Find(x => x.Id == id);
            if (p == null) return;

            Console.Write("Nytt pris: ");
            p.Price = double.Parse(Console.ReadLine());

            Console.WriteLine("Priset har ändrats.");
            Console.ReadLine();
        }


        // ADMIN ÄNDRA LAGERANTAL

        static void AdminAndraLager()
        {
            VisaProdukter();

            Console.Write("Produkt-ID: ");
            int id = int.Parse(Console.ReadLine());

            Product p = products.Find(x => x.Id == id);
            if (p == null) return;

            Console.Write("Nytt lagerantal: ");
            p.Stock = int.Parse(Console.ReadLine());

            Console.WriteLine("Lagerantalet har ändrats.");
            Console.ReadLine();
        }
        // ADMIN TA BORT EN PRODUKT
        static void AdminTaBortProdukt()
        {
            VisaProdukter();

            Console.Write("Ange produkt-ID att ta bort: ");
            int id = int.Parse(Console.ReadLine());

            Product p = products.Find(x => x.Id == id);

            if (p == null)
            {
                Console.WriteLine("Produkten finns inte.");
                Console.ReadLine();
                return;
            }

            products.Remove(p);

            Console.WriteLine("Produkten har tagits bort.");
            Console.ReadLine();
        }

        // ADMIN LÄGG TILL EN PRODUKT
        static void AdminLaggTillProdukt()
        {
            try
            {
                Console.Clear();

                Console.Write("Produktnamn: ");
                string name = Console.ReadLine();

                Console.Write("Pris: ");
                double price = double.Parse(Console.ReadLine());

                Console.Write("Lagerantal: ");
                int stock = int.Parse(Console.ReadLine());

                int newId = products.Count + 1;

                products.Add(new Product
                {
                    Id = newId,
                    Name = name,
                    Price = price,
                    Stock = stock
                });

                Console.WriteLine("Produkten har lagts till.");
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Fel inmatning. Använd siffror.");
                Console.ReadLine();
            }
        }


        // ===== KVITTO =====
        static void VisaKvitto()
        {
            Console.Clear();
            double total = 0;

            Console.WriteLine("KVITTO\n");
            foreach (CartItem c in cart)
            {
                double sum = c.Product.Price * c.Quantity;
                Console.WriteLine($"{c.Product.Name} x{c.Quantity} = {sum} kr");
                total += sum;
            }

            Console.WriteLine($"\nTOTALT: {total} kr");
            Console.WriteLine("\nTack för ditt köp!");
            Console.ReadLine();
        }
    }
}
