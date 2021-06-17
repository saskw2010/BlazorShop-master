namespace BlazorShop.Models.Categories
{
    using System.Collections.Generic;

    public class CategoriesSearchResponseModel
    {
        public IEnumerable<CategoriesListingResponseModel> Categories { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
