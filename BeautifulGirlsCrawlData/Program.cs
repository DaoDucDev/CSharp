using System;
using System.Collections.Generic;

namespace BeautifulGirlsCrawlData
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await Services.CreateJsonFileAsync();
        }
    }
}
