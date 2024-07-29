using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionTracker.Web.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Name { get; set; }
        public string Condition { get; set; }
        public bool Sealed { get; set; }
        public string Platform { get; set; }
        public string MediaType { get; set; }
        public string Complete { get; set; }
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

        public List<SelectListItem> MediaLst = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Blu-ray", Value = "Blu-ray"},
            new SelectListItem {Text = "DVD-ROM", Value = "DVD-ROM"},
            new SelectListItem {Text = "CD-ROM", Value = "CD-ROM"},
            new SelectListItem { Text = "Tape", Value = "Tape" },
            new SelectListItem { Text = "3.5 Disk", Value = "3.5 Disk" },
            new SelectListItem { Text = "5.25 Disk", Value = "5.25 Disk" }
        };

        public List<SelectListItem> PlatformLst = new List<SelectListItem>()
        {
            new SelectListItem {Text = "PC", Value = "PC"},
            new SelectListItem {Text = "MAC", Value = "MAC"},
            new SelectListItem {Text = "Amiga", Value = "Amiga"},
            new SelectListItem {Text = "Atari ST", Value = "Atari ST"},
            new SelectListItem {Text = "Spectrum", Value = "Spectrum"},
            new SelectListItem {Text = "Amstrad", Value = "Amstrad"},
            new SelectListItem {Text = "C64", Value = "C64"},
            new SelectListItem {Text = "Xbox 360", Value = "Xbox 360"},
            new SelectListItem {Text = "Xbox One", Value = "Xbox One"},
            new SelectListItem {Text = "Playstation 2", Value = "Playstation 2"},
            new SelectListItem {Text = "Playstation 4", Value = "Playstation 4"},
            new SelectListItem {Text = "Mongrel", Value = "Mongrel"}
        };

        public List<SelectListItem> CompleteLst = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Yes", Value = "Yes"},
            new SelectListItem {Text = "No", Value = "No"},
            new SelectListItem {Text = "Unknown", Value = "Unknown"},
        };
    }
}