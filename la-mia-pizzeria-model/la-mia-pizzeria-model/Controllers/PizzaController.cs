using la_mia_pizzeria_model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var pizza = ctx.Pizzas
                .Include(p => p.Category)
                .SingleOrDefault(p => p.Id == id);

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

            return View(formModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel form)
        {
            using var ctx = new PizzaContext();

            if (!ModelState.IsValid)
            {
                form.Categories = ctx.Categories.ToArray();

                return View(form);
            }

            ctx.Pizzas.Add(form.Pizza);

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

            var formModel = new PizzaFormModel
            {
                Pizza = pizza,

                Categories = ctx.Categories.ToArray(),
            };

            return View(formModel);
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update(int id, PizzaFormModel form)
        {
            using var ctx = new PizzaContext();

            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var pizzaToUpdate = ctx.Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizzaToUpdate is null)
            {
                return NotFound();
            }

            pizzaToUpdate.Name = form.Pizza.Name;
            pizzaToUpdate.Description = form.Pizza.Description;
            pizzaToUpdate.Price = form.Pizza.Price;
            pizzaToUpdate.ImgSrc = form.Pizza.ImgSrc;
            pizzaToUpdate.Category = form.Pizza.Category;

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