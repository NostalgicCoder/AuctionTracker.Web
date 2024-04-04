using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionTracker.Web.Models
{
    public class Toy
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }
        public string? Colour { get; set; }
        public string? DamagedAccessory { get; set; }
        public string? Stands { get; set; }

        [Required]
        public string ToyLine { get; set; }
        public string Name { get; set; }
        public string Condition { get; set; }
        public string Complete { get; set; }
        public string Damaged { get; set; }
        public bool Discoloured { get; set; }
        public bool Carded { get; set; }
        public bool Boxed { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Postage { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.Now;

        public List<SelectListItem> ConditionList = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Mint", Value = "Mint"},
            new SelectListItem {Text = "High", Value = "High"},
            new SelectListItem {Text = "Medium", Value = "Medium"},
            new SelectListItem {Text = "Low", Value = "Low"}
        };

        public List<SelectListItem> CompleteLst = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Yes", Value = "Yes"},
            new SelectListItem {Text = "No", Value = "No"},
            new SelectListItem {Text = "Unknown", Value = "Unknown"}
            //new SelectListItem {Text = "No", Value = "No",Selected = true} // Example
        };

        public List<SelectListItem> ColourLst = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Neon Green", Value = "Neon Green"},
            new SelectListItem {Text = "Neon Purple", Value = "Neon Purple"},
            new SelectListItem {Text = "Neon Red", Value = "Neon Red"},
            new SelectListItem {Text = "Neon Yellow", Value = "Neon Yellow"},
            new SelectListItem {Text = "Glow In Dark", Value = "Glow In Dark"},
            new SelectListItem {Text = "Cyan", Value = "Cyan"},
            new SelectListItem {Text = "Pine Green", Value = "Pine Green"},
            new SelectListItem {Text = "Olive Green", Value = "Olive Green"},
            new SelectListItem {Text = "Purple", Value = "Purple"},
            new SelectListItem {Text = "Yellow", Value = "Yellow"},
            new SelectListItem {Text = "Pink", Value = "Pink"},
            new SelectListItem {Text = "Red", Value = "Red"},
            new SelectListItem {Text = "Light Orange", Value = "Light Orange"},
            new SelectListItem {Text = "Orange", Value = "Orange"}
        };

        public List<SelectListItem> ToyLineLst = new List<SelectListItem>();
    }
}
