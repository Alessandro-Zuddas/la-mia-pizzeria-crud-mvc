using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_model.Models
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaDb;Integrated Security=True;Pooling=False;TrustServerCertificate = True");
        }

        public void Seed()
        {
            if (!Pizzas.Any())
            {
                var seed = new Pizza[]
                {
                    new Pizza
                    {
                        ImgSrc = "/img/pizza-margherita.jpg",
                        Name = "Pizza Margherita",
                        Description = "La classica pizza italiana",
                        Price = "4.99 €"
                    },
                    new Pizza
                    {
                        ImgSrc = "/img/pizza-americana.jpg",
                        Name = "Pizza Americana",
                        Description = "La pizza con patatine e wurstel",
                        Price = "6.99 €"
                    },
                    new Pizza
                    {
                        ImgSrc = "/img/pizza-napoli.jpg",
                        Name = "Pizza Napoli",
                        Description = "La preferita dai napoletani",
                        Price = "5.99 €"
                    },
                };

                Pizzas.AddRange(seed);

                SaveChanges();
            }
        }
    }
}
