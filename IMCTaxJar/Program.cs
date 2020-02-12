using IMCTaxJar.Models;
using IMCTaxJar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMCTaxJar
{
    class Program
    {

        private static IList<Product> products { get; set; }

        static async Task Main(string[] args)
        {
            CreateProductList();

            while (true)
            {
                var order = new Order(new TaxJarService());

                Console.WriteLine("Enter To Zip");
                order.ToZip = Console.ReadLine();
                Console.WriteLine("Enter To State");
                order.ToState = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter From Zip");
                order.FromZip = Console.ReadLine();
                Console.WriteLine("Enter From State");
                order.FromState = Console.ReadLine();

                var selectedItem = 0;

                while (selectedItem == 0)
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Id} {product.Name}");
                    }

                    Console.WriteLine("Select a product to add or press 0 to calulate");

                    int productIdToAdd = 0;

                    int.TryParse(Console.ReadLine(), out productIdToAdd);

                    if (productIdToAdd == 0)
                    {
                        break;
                    }
                    else
                    {
                        var productToAdd = products.FirstOrDefault(p => p.Id == productIdToAdd);
                        if (productToAdd != null)
                        {
                            order.AddProduct(productToAdd);
                            Console.WriteLine($"{productToAdd.Name} was added");
                        }
                        else
                            Console.WriteLine($"Product {productIdToAdd} was not found");
                    }
                }

                Console.WriteLine("--------");
                Console.WriteLine($"Subtotal: {order.GetSubTotal()}");
                Console.WriteLine($"Tax Percentage: {await order.GetTaxPercentage()}");
                Console.WriteLine($"Tax Amount: {await order.GetTaxAmount()}");
                Console.WriteLine($"Order Total: {await order.GetTotal()}");
                Console.WriteLine("--------");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press ENTER to continue");
                Console.ReadLine();
                Console.Clear();
            }

        }


        private static void CreateProductList()
        {
            products = new List<Product>();

            products.Add(new Product() { Id = 1, Name = "Product 1", Price = 10.99m });
            products.Add(new Product() { Id = 2, Name = "Product 2", Price = 12.99m });
            products.Add(new Product() { Id = 3, Name = "Product 3", Price = 10.99m });
            products.Add(new Product() { Id = 4, Name = "Product 4", Price = 14.99m });
            products.Add(new Product() { Id = 5, Name = "Product 5", Price = 20.99m });
            products.Add(new Product() { Id = 6, Name = "Product 6", Price = 99.99m });

        }
    }
}
