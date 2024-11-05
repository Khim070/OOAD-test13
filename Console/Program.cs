using productlib;

namespace ProductConsole;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Product Management System");
        ProductHelper.MenuBank.MenuSimulate(() => Console.WriteLine());
    }
}
