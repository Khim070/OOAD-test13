using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MenuLib;
using RestClientLib;
using ProductLib;
using ProductConsole;

namespace productlib
{
    public static class ProductHelper
    {
        public static string BaseUri { get; }
        private static string ProductRoute { get; }

        static ProductHelper()
        {
            var json = File.ReadAllText("appsettings.json");
            var config = JsonSerializer.Deserialize<AppConfig>(json);
            BaseUri = config?.BaseUri ?? string.Empty;
            ProductRoute = config?.BookRoute ?? string.Empty;
        }

        public static MenuBank MenuBank { get; set; } = new MenuBank()
        {
            Title = "Products",
            Menus = new List<Menu>()
            {
                new Menu(){ Text= "Viewing", Action=Viewing},
                new Menu(){ Text= "Creating", Action=Creating},
                new Menu(){ Text= "Updating", Action=Updating},
                new Menu(){ Text= "Deleting", Action=Deleting},
                new Menu(){ Text= "Exiting", Action = Exiting}
            }
        };

        

        public static void Exiting()
        {
            Console.WriteLine("\n[Exiting Program]");
            Environment.Exit(0);
        }

        private static void Deleting()
        {
            Console.WriteLine("\n[Deleting]");
            while (true)
            {
                Task.Run(async () =>
                {
                    Console.Write("Product Id/Code: ");
                    var key = Console.ReadLine() ?? "";
                    using (var client = new RestClient(BaseUri))
                    {
                        try
                        {
                            string endpoint = $"{ProductRoute}/{key}";
                            var result = await client.DeleteAsync<Result<bool>>(endpoint);
                            if (result?.Succeded ?? false)
                            {
                                Console.WriteLine($"Successfully delete the product with id/code {key}");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to delete the product with id/code {key}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error>{ex.Message}");
                        }
                    }
                }).Wait();
                Console.WriteLine();
                if (WaitForEscPressed("ESC to stop or any key for more deleting...")) break;
            }

        }

        private static void Updating()
        {
            Console.WriteLine("\n[Updating]");
            while (true)
            {
                Task.Run(async () =>
                {
                    Console.Write("Product Id/Code(required): ");
                    var key = Console.ReadLine() ?? "";
                    using (var client = new RestClient(BaseUri))
                        try
                        {
                            // Getting an existing product of given key
                            string endpoint = $"{ProductRoute}/{key}";
                            var result = await client.GetAsync<Result<ProductResponse?>>(endpoint);

                            if (result?.Succeded ?? false) // found
                            {
                                var prd = result.Data;
                                Console.Write($"Current name:{prd?.Name} > New name (optional) :");
                                var name = Console.ReadLine();

                                Console.WriteLine($"Category Available: {Enum.GetNames<Category>().Aggregate((a, b) => a + "," + b)}");
                                Console.Write($"Current category:{prd?.Category} > New Category: ");
                                var category = Console.ReadLine();
                                var req = new ProductUpdateReq
                                {
                                    Key = key,
                                    Name = name,
                                    Category = category
                                };

                                // Update an exisiting product
                                endpoint = $"{ProductRoute}";
                                var result2 = await client.PutAsync<ProductUpdateReq, Result<bool>>(endpoint, req);
                                Console.WriteLine($"{result2?.Message}");
                            }
                            else // not found
                            {
                                Console.WriteLine($"{result?.Message}");
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine($"Error>{ex.Message}");
                        }
                }).Wait();
                Console.WriteLine();
                if (WaitForEscPressed("ESC to stop or any key for more updating...")) break;
            }
        }

        private static bool WaitForEscPressed(string text)
        {
            Console.Write(text); ;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.WriteLine();
            return keyInfo.Key == ConsoleKey.Escape;
        }

        private static void Creating()
        {
            Console.WriteLine("\n[Creating]");
            while (true)
            {
                Task.Run(async () =>
                {
                    var req = GetCreateProduct();
                    if(req != null)
                    {
                        using(var client = new RestClient(BaseUri))
                        {
                            var endpoint = $"{ProductRoute}";
                            try
                            {
                                var result = await client.PostAsync<ProductCreateReq, Result<string?>>(endpoint, req);
                                if(result?.Succeded ?? false)
                                {
                                    Console.WriteLine($"Successfully create a new product with id, {result.Data}");
                                }
                                else
                                {
                                    Console.WriteLine($"{result?.Message}");
                                }
                            }catch(Exception ex)
                            {
                                Console.WriteLine($"Error>{ex.Message}");
                            }
                        }
                    }
                }).Wait();
                Console.WriteLine();
                if (WaitForEscPressed("ESC to stop or any key for more creating...")) break;
            }
        }

        private static void Viewing()
        {
            Console.WriteLine("\n[Viewing]");
            Task.Run(async () =>
            {
                using (var client = new RestClient(BaseUri))
                {
                    var endpoint = $"{ProductRoute}";
                    try
                    {
                        var result = await client.GetAsync<Result<List<ProductResponse>>>(endpoint);
                        var all = result?.Data ?? [];
                        var count = result?.Data?.Count() ?? 0;
                        Console.WriteLine($"Products: {count}");
                        if (count == 0) return;
                        Console.WriteLine($"{"Id",-36} {"Code",-10} {"Name",-10} {"Category",-20}");
                        foreach (var prd in all)
                        {
                            Console.WriteLine($"{prd.Id,-36} {prd.Code,-10} {prd.Name,-10} {prd.Category,-20}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error>{ex.Message}");
                    }
                }
            }).Wait();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        private static ProductCreateReq? GetCreateProduct()
        {
            Console.WriteLine($"Category available: {Enum.GetNames<Category>().Aggregate((a, b) => a + "," + b)}");
            Console.Write("Product(code/name/category): ");
            string data = Console.ReadLine() ?? "";
            var dataParts = data.Split('/');
            if (dataParts.Length < 3) 
            {
                Console.WriteLine("Invalid create product's data");
            }
            var code = dataParts[0].Trim();
            var name = dataParts[1].Trim();
            var categroy = dataParts[2].Trim();

            return new ProductCreateReq()
            {
                Code = code,
                Name = name,
                Category = categroy,
            };
        }
    }
}
