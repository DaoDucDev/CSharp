using System.Threading.Tasks;

namespace Presentation
{
    class Program
    {

        static async Task Main(string[] args)
        {
            do
            {
                await Menu.DisplayMainMenuAsync();
            } while (true);
            
        }
    }
}
