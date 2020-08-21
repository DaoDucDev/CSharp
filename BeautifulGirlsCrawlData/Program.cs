using System;
using System.Collections.Generic;

namespace BeautifulGirlsCrawlData
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            List<Post> posts = await Services.GetAllPost();
        }
    }
}
