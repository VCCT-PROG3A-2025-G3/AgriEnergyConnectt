using Microsoft.AspNetCore.Identity;

namespace AgriEnergyConnectt.Models.ViewModels
{
    public class FarmerProfileViewModel
    {
        public IdentityUser Farmer { get; set; }
        public List<Product> Products { get; set; }
    }
}
