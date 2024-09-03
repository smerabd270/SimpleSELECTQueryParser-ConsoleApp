using System;

namespace SimpleSELECTQueryParser_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new SQLParser("SELECT user_id, username FROM users WHERE user_id = 123 AND username = 'John'");
            Console.WriteLine(parser.Parse());

            parser = new SQLParser("SELECT user_id, username, age_id FROM users WHERE user_id = 123 AND age_id = '25'");
            Console.WriteLine(parser.Parse());
        }
    }
}
