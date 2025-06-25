using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnectt.Models.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

       
    }
}
