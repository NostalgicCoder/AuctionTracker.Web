using AuctionTracker.Web.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AuctionTracker.Web.Models
{
    public class Product
    {
        public decimal MeanPrice { get; set; }
        public decimal MeanPriceAndPostage { get; set; }
        public decimal MedianPrice { get; set; }
        public decimal MedianPriceAndPostage { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MaxPriceAndPostage { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MinPriceAndPostage { get; set; }
        public decimal MaxPriceCurrentYear { get; set; }
        public decimal MaxPriceAndPostageCurrentYear { get; set; }
        public decimal MinPriceCurrentYear { get; set; }
        public decimal MinPriceAndPostageCurrentYear { get; set; }
        public decimal MeanPostage { get; set; }
        public decimal MedianPostage { get; set; }
        public decimal MaxPostage { get; set; }
        public decimal MinPostage { get; set; }
        public decimal MaxPostageCurrentYear { get; set; }
        public decimal MinPostageCurrentYear { get; set; }


        public string Trend { get; set; }
        public string SelectedProduct { get; set; }
        public string SelectedProductLine { get; set; }
        public string SelectedSortOrder { get; set; }
        public string SearchCriteria { get; set; }

        public bool SearchCondition { get; set; }
        public bool SearchComplete { get; set; }
        public bool SearchCurrentYear { get; set; }
        public bool SearchLoose { get; set; }
        public bool SearchCardedBoxed { get; set; }

        public IEnumerable<Game> Game { get; set; }
        public IEnumerable<Toy> Toy { get; set; }

        public List<SelectListItem> SortOrderListGame = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Price > (Low to High)", Value = "PriceAsc"},
            new SelectListItem {Text = "Price < (High to Low)", Value = "PriceDsc"},
            new SelectListItem {Text = "Date (Old to New)", Value = "DateAsc"},
            new SelectListItem {Text = "Date (New to Old)", Value = "DateDsc"},
            new SelectListItem {Text = "Name", Value = "Name"},
            new SelectListItem {Text = "Condition", Value = "Condition"},
            new SelectListItem {Text = "Complete", Value = "Complete"},
            new SelectListItem {Text = "Platform", Value = "Platform"},
            new SelectListItem {Text = "Sealed", Value = "Sealed"}
        };

        public List<SelectListItem> SortOrderListToy = new List<SelectListItem>()
        {
            new SelectListItem {Text = "Price > (Low to High)", Value = "PriceAsc"},
            new SelectListItem {Text = "Price < (High to Low)", Value = "PriceDsc"},
            new SelectListItem {Text = "Date (Old to New)", Value = "DateAsc"},
            new SelectListItem {Text = "Date (New to Old)", Value = "DateDsc"},
            new SelectListItem {Text = "Name", Value = "Name"},
            new SelectListItem {Text = "Condition", Value = "Condition"},
            new SelectListItem {Text = "Complete", Value = "Complete"},
            new SelectListItem {Text = "Carded", Value = "Carded"},
            new SelectListItem {Text = "Boxed", Value = "Boxed"},
            new SelectListItem {Text = "Damaged", Value = "Damaged"},
            new SelectListItem {Text = "Damaged Accessory", Value = "DamagedAccessory"}
        };

        public List<IGraphModel> LineGraph = new List<IGraphModel>();
    }
}