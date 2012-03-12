using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;

namespace AspNetWebApi.Model.Entities
{
    public class StockContext : DbContext
    {
        public DbSet<StockCacheItem> StockCacheItems {get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<StockSymbol> StockSymbols { get; set; }
    }


    /// <summary>
    /// Used to create sample data upon database installation
    /// </summary>       
    public class StockContextInitializer :  
        //CreateDatabaseIfNotExists<StockContext>
        DropCreateDatabaseIfModelChanges<StockContext>
    {

        protected override void Seed(StockContext context)
        {
            string error = string.Empty;

            var users = new List<User>() {            
                new User()
                {
                   Id=1,
                   Name = "Rick Strahl",
                   Email = "rick@west-wind.com",
                   Password = "seekret"
                },
                new User()
                {
                   Id = 2,
                   Name = "Guest",
                   Email = "guest@guest.com",
                   Password = "secret"
                }
            };

            users.ForEach(cat => context.Users.Add(cat));

            
            try
            {
            	context.SaveChanges();
            }
            catch (Exception ex)
            {
            	error += error + ex.GetBaseException().Message + "\r\n";
            }

            var time = new DateTime(2012,3,10,11,20,0);

            var portfolioItems = new List<PortfolioItem>() {
                new PortfolioItem()
                {
                    UserId = 1,
                    Symbol = "MSFT",
                    Company = "Microsoft Corporation",
                    Qty = 50,
                    LastPrice = 30M,
                    ItemValue = 150M,
                    QuoteTime = time
                },
                new PortfolioItem()
                {
                    UserId = 2,
                    Symbol = "MSFT",
                    Company = "Microsoft Corporation",
                    Qty = 50,
                    LastPrice = 30M,
                    ItemValue = 150M,
                    QuoteTime = time
                },
                new PortfolioItem()
                {
                    UserId = 1,
                    Symbol = "INTL",
                    Company = "Intel Corporation",
                    Qty = 80,
                    LastPrice = 27M,
                    ItemValue = 27M * 80,
                    QuoteTime = time
                },
                new PortfolioItem()
                {
                    UserId = 1,
                    Symbol = "GLD",
                    Company = "Gold SPR",
                    Qty = 100,
                    LastPrice = 165.20M,
                    ItemValue = 16500M,
                    QuoteTime = time
                },
                new PortfolioItem()
                {
                    UserId = 1,
                    Symbol = "SLW",
                    Company = "Silver Wheaton",
                    Qty = 100,
                    LastPrice = 31.20M,
                    ItemValue = 3120M,
                    QuoteTime = time
                }
            };

            portfolioItems.ForEach( item=> context.PortfolioItems.Add(item));

            try
            {
            	context.SaveChanges();
            }
            catch (Exception ex)
            {
            	error += error + ex.GetBaseException().Message + "\r\n";
            }

            var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/symbollist.txt"));
            
            string current = string.Empty;
            while(current != null)
            {
                current = reader.ReadLine();
                if (current == null)
                    continue;

                var tokens = current.Split(',');

                if (tokens.Length == 2)
                {
                    context.Database.ExecuteSqlCommand("insert into StockSymbols (Symbol,Company) values ({0},{1})", tokens[0], tokens[1]);
                    //context.StockSymbols.Add(new StockSymbol() { Symbol = tokens[0], Company = tokens[1] });
                    //context.SaveChanges();
                }
            }
        }
    }
}