using SwaggerServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClient
{
    class Program
    {


        static void Main(string[] args)
        {
            // Verwenden Sie hier Ihren Proxy ...

            FlugWebProxy proxy = new FlugWebProxy(new Uri("http://localhost:4845"));

            var flug = new Flug {
                AblugOrt = "A",
                ZielOrt = "B",
                Datum = DateTime.Now,
                FlugNr = "4711"
            };

            proxy.ApiFlugPost(flug).Wait();

            var fluege = proxy.ApiFlugGet().Result;


            Console.WriteLine("Fertig!");
            Console.ReadLine();
            
        }
    }
}
