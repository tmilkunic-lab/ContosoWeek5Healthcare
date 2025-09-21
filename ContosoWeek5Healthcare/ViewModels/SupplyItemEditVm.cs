using System.ComponentModel.DataAnnotations;

namespace ContosoWeek5Healthcare.ViewModels
{
    public class SupplyItemEditVm
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string ItemNumber { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string UnitOfMeasure { get; set; } = "EA";

        [Range(0, 365)]
        public int LeadTimeDays { get; set; } = 7;

        [Range(0, 100000)]
        public int ParLevel { get; set; }

        [Range(0, 100000)]
        public int ReorderPoint { get; set; }

        [Range(0, 100000)]
        public int OnHand { get; set; }
    }
}
