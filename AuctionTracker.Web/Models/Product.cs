using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Models
{
    public class Product
    {
        public decimal MeanPrice { get; set; }
        public decimal MedianPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPriceCurrentYear { get; set; }
        public decimal MinPriceCurrentYear { get; set; }
        public decimal MeanPostage { get; set; }
        public decimal MedianPostage { get; set; }
        public decimal MaxPostage { get; set; }
        public decimal MinPostage { get; set; }

        public string SelectedProduct { get; set; }
        public string SelectedProductLine { get; set; }
        public string SelectedSortOrder { get; set; }

        public IEnumerable<Game> Game { get; set; }
        public IEnumerable<Toy> Toy { get; set; }

        public List<SelectListItem> SortOrderList = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Price >", Value = "PriceAsc"},
            new SelectListItem {Text = "Price <", Value = "PriceDsc"},
            new SelectListItem {Text = "Platform", Value = "Platform"},
            new SelectListItem {Text = "Name", Value = "Name"},
            new SelectListItem {Text = "Date >", Value = "DateAsc"},
            new SelectListItem {Text = "Date <", Value = "DateDsc"},
            new SelectListItem {Text = "Condition", Value = "Condition"},
            new SelectListItem {Text = "Complete", Value = "Complete"}
        };
    }
}
