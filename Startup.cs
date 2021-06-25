using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using static System.Console;

namespace MongoDB
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Init() 
        {
            var _db = new AppDbContext();

            WriteLine("DropCollection");
            _db.Database.DropCollection("Pessoas");

            ReadKey();

            WriteLine("CreateCollection");
            _db.Database.CreateCollection("Pessoas");

            while (ReadKey().Key != ConsoleKey.Escape)
            {
                WriteLine("DeleteMany");
                _db.Pessoas.DeleteMany(x => true);

                var pessoa = new Pessoa{
                    Id = Guid.NewGuid(),
                    Nome = "Maria José da Silva"
                };

                WriteLine("InsertOne");
                _db.Pessoas.InsertOne(pessoa);

                var pessoas = _db.Pessoas.Find(x => x.Nome.Contains("Maria")).ToList();

                foreach (var p in pessoas)
                {
                    WriteLine(p.Nome);
                }
            }            
        }
    }
}