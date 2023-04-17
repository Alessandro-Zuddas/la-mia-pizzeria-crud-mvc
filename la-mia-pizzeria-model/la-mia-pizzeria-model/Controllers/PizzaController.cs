using la_mia_pizzeria_model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace la_mia_pizzeria_model.Controllers
{
    public class PizzaController : Controller
    {
        private readonly ILogger<PizzaController> _logger;

        public PizzaController(ILogger<PizzaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var ctx = new PizzaContext();

            var pizze = ctx.Pizzas.ToArray();

            return View(pizze);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            using var ctx = new PizzaContext();

            var pizza = ctx.Pizzas.SingleOrDefault(p => p.Id == id);

            if(pizza == null)
            {
                return NotFound("Pizza non trovata!");
            }

            return View(pizza);
        }

        public IActionResult Create()
        {
			using var ctx = new PizzaContext();

			var formModel = new PizzaFormModel
            {
                Categories = ctx.Categories.ToArray(),
            }; 

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(!ModelState.IsValid)
            {
                return View(pizza);
            }

            using var ctx = new PizzaContext();
            ctx.Pizzas.Add(pizza);

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            using var ctx = new PizzaContext();
            
            var pizza = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizza == null)
            {
                return NotFound();
            }

            return View(pizza);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, Pizza pizza)
        {
            using var ctx = new PizzaContext();

            if (!ModelState.IsValid)
            {
                return View(pizza);
            }

            var pizzaToUpdate = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaToUpdate is null)
            {
                return NotFound();
            }

            pizzaToUpdate.Name = pizza.Name;
            pizzaToUpdate.Description = pizza.Description;
            pizzaToUpdate.Price = pizza.Price;
            pizzaToUpdate.ImgSrc = pizza.ImgSrc;

            ctx.SaveChanges();

            return RedirectToAction("Index");
        }


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(int id)
        {
			using var ctx = new PizzaContext();

            var pizzaToDelete = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaToDelete is null)
            {
                return NotFound();
            }

            ctx.Pizzas.Remove(pizzaToDelete);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}