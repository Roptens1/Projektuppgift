using System;
using System.Collections.Generic;

namespace Projektuppgift
{
    class Program
    {
        // Kundvagn
        static List<Product> cart = new List<Product>();
        // Produktlista
        static List<Product> products = new List<Product>()
        {
               new Product { Id = 1, Name = "Mjölk", Price = 15, Stock = 20 },
               new Product { Id = 2, Name = "Bröd", Price = 25, Stock = 10 },
               new Product { Id = 3, Name = "Ägg",  Price = 30, Stock = 50 }
        };

        static void Main(string[] args)
        {
            bool run = true;

            while (run)
            {
                Console.Clear();
                Console.WriteLine("MENY");
                Console.WriteLine("1. Visa produkter");
                Console.WriteLine("2. Lägg till_produkt i kundvagn");
                Console.WriteLine("3. Visa kundvagn");
                Console.WriteLine("4. Ta bort produkt");
                Console.WriteLine("7. Avsluta");
                Console.Write("Välj: ");

                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        VisaProdukter();
                        break;
                    case "2":
                        Lägg_till_produkt_i_kundvagn();
                        break;

                    case "3":
                        Visa_kundvagn();
                        break;



                    case "7":
                        run = false;
                        break;

                    default:
                        Console.WriteLine("Fel val. Tryck Enter.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // METODEN för att visa produkter när man trycker 1 i menyn
        static void VisaProdukter()
        {
            Console.Clear();
            Console.WriteLine("Produkter:\n");

            foreach (Product p in products)
            {
                Console.WriteLine(
                    $"ID: {p.Id} | {p.Name} | {p.Price} kr | Lager: {p.Stock}"
                );
            }

            Console.WriteLine("\nTryck Enter för att fortsätta.");
            Console.ReadLine();
        }



        // METODEN för att lägga till produkt i kundvagn när man trycker 2 i menyn
        static void Lägg_till_produkt_i_kundvagn()
        {
            Console.Clear();
            VisaProdukter();

            Console.Write("\nAnge produkt-ID: ");
            string idInput = Console.ReadLine();

            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Felaktigt ID.");
                Console.ReadLine();
                return;
            }

            Product found = products.Find(p => p.Id == id);

            if (found == null)
            {
                Console.WriteLine("Produkten finns inte.");
                Console.ReadLine();
                return;
            }

            Console.Write($"Hur många {found.Name} vill du lägga till? ");
            string qtyInput = Console.ReadLine();

            if (!int.TryParse(qtyInput, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Felaktigt antal.");
                Console.ReadLine();
                return;
            }

            if (quantity > found.Stock)
            {
                Console.WriteLine("Det finns inte så många i lager.");
                Console.ReadLine();
                return;
            }

            // Lägg produkten flera gånger
            for (int i = 0; i < quantity; i++)
            {
                cart.Add(found);
            }

            found.Stock -= quantity;

            Console.WriteLine($"{quantity} st {found.Name} lades till i kundvagnen.");
            Console.ReadLine();
        }


        // METODEN för att visa kund vagn när man trycker 3 i menyn
        static void Visa_kundvagn()
        {
            Console.Clear();

            if (cart.Count == 0)
            {
                Console.WriteLine("Kundvagnen är tom.");
            }
            else
            {
                double total = 0;
                Console.WriteLine("Kundvagn:\n");

                foreach (Product p in cart)
                {
                    Console.WriteLine($"{p.Name} - {p.Price} kr");
                    total += p.Price;
                }

                Console.WriteLine($"\nTotalpris: {total} kr");
            }

            Console.WriteLine("\nTryck Enter.");
            Console.ReadLine();
        }


    }
}
