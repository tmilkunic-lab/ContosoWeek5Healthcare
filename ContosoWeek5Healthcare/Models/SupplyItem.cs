using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoWeek5Healthcare.Models  
{
    public class SupplyItem : IValidatableObject
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        [Display(Name = "Item #")]
        public string ItemNumber { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required, StringLength(10)]
        [Display(Name = "UOM")]
        public string UnitOfMeasure { get; set; } = "EA";

        [Range(0, 365)]
        [Display(Name = "Lead Time (days)")]
        public int LeadTimeDays { get; set; } = 7;

        [Range(0, 100000)]
        [Display(Name = "Par Level")]
        public int ParLevel { get; set; }

        [Range(0, 100000)]
        [Display(Name = "Reorder Point (ROP)")]
        public int ReorderPoint { get; set; }

        [Range(0, 100000)]
        [Display(Name = "On Hand")]
        public int OnHand { get; set; }

        
        public bool IsLowStock() => OnHand <= ReorderPoint;

        public IEnumerable<ValidationResult> Validate(ValidationContext _)
        {
            if (ReorderPoint > ParLevel)
                yield return new ValidationResult("ROP must be ≤ Par Level.", new[] { nameof(ReorderPoint) });
        }
    }
}
