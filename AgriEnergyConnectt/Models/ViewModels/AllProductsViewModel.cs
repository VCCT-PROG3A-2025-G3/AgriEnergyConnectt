namespace AgriEnergyConnectt.Models.ViewModels
{
    public class AllProductsViewModel
    {
        public List<Product> Products { get; set; }
        public List<string> AvailableCategories { get; set; }
        public ProductFilters Filters { get; set; }
    }

    public class ProductFilters
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public DateTime? ProductionDate { get; set; }
    }
}
