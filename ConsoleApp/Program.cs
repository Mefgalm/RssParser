using System;
using System.Linq;
using RssParser;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = RssExtractor.GetItemsAsync("https://career.habr.com/vacancies/rss?page=1&per_page=25").Result.ToList();
            
            items.ForEach(Console.WriteLine);
        }
    }
}