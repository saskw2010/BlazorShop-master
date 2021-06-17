﻿namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Models.Categories;
    using Models.Products;
    public partial class Index
    {
        private IEnumerable<ProductsListingResponseModel> products;
        private IEnumerable<CategoriesListingResponseModel> categories;

        protected override async Task OnInitializedAsync()
        {
            var response = await this.Http.GetFromJsonAsync<ProductsSearchResponseModel>("api/products");

            this.products = response.Products;
             categories = await this.CategoriesService.All();

        }
        

       
            
    }
}
