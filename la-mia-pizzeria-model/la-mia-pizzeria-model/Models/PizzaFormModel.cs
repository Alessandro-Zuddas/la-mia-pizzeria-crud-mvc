namespace la_mia_pizzeria_model.Models
{
	public class PizzaFormModel
	{
		public Pizza Pizza { get; set; }
		public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
	}
}
