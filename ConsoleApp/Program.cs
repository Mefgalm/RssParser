using System;
using System.Linq;
using RssParser;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = RssExtractor.GetItemsAsync("https://habr.com/ru/rss/all/all/").Result.ToList();
            
            items.ForEach(Console.WriteLine);
        }
    }
}