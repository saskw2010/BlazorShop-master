using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

using BlazorShop.Models.Categories;
using BlazorShop.Models.Products;
using BlazorShop.Models.ShoppingCarts;
namespace BlazorShop.Web.Client.Pages.Categories
{
    public partial class Categorieslist
    {

        private readonly ProductsSearchRequestModel model = new ProductsSearchRequestModel();

        private ProductsSearchResponseModel searchResponse;
        private IEnumerable<ProductsListingResponseModel> products;
        private IEnumerable<CategoriesListingResponseModel> categories;

        [Parameter]
        public int? CategoryId { get; set; }

        [Parameter]
        public string CategoryName { get; set; }

        [Parameter]
        public string SearchQuery { get; set; } = string.Empty;

        [Parameter]
        public int Page { get; set; } = 1;

        [Parameter]
        public bool ListView { get; set; } = false;

        [Parameter]
        public bool GridView { get; set; } = true;

        protected override async Task OnInitializedAsync() => await this.LoadData();

        protected override async Task OnParametersSetAsync() => await this.LoadData(withCategories: false);

        private async Task SelectedPage(int page)
        {
            this.Page = page;

            await this.LoadData(withCategories: false);
        }

        private async Task LoadData(bool withCategories = true)
        {
            if (this.Page == 0)
            {
                this.Page = 1;
            }

            this.model.Category = this.CategoryId;
            this.model.Query = this.SearchQuery;
            this.model.Page = this.Page;

            this.searchResponse = await this.ProductsService.SearchAsync(this.model);
            this.products = this.searchResponse.Products;

            if (withCategories)
            {
                this.categories = await this.CategoriesService.All();
            }

            this.Filter();
        }

        private async Task AddToWishlist(int id)
        {
            await this.WishlistsService.AddProduct(id);
            this.NavigationManager.NavigateTo("/wishlist");
        }

        private async Task AddToCart(int id)
        {
            var cartRequest = new ShoppingCartRequestModel
            {
                ProductId = id,
                Quantity = 1
            };

            var result = await this.ShoppingCartsService.AddProduct(cartRequest);

            if (!result.Succeeded)
            {
                this.ToastService.ShowError(result.Errors.First());
            }
            else
            {
                this.NavigationManager.NavigateTo("/cart", forceLoad: true);
            }
        }

        private void ChangeView()
        {
            this.ListView = !this.ListView;
            this.GridView = !this.GridView;
        }

        private void Reset()
        {
            this.model.MinPrice = null;
            this.model.MaxPrice = null;
            this.NavigationManager.NavigateTo("/categories/page/1");
        }

        private void Filter()
        {
            if (!string.IsNullOrWhiteSpace(this.model.Query) && string.IsNullOrWhiteSpace(this.CategoryName) && !this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/categories/search/{this.model.Query}/page/{this.model.Page}");
            }
            else if (!string.IsNullOrWhiteSpace(this.model.Query) && !string.IsNullOrWhiteSpace(this.CategoryName) && this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/categories/category/{this.CategoryName}/{this.model.Category}/search/{this.model.Query}/page/{this.model.Page}");
            }
            else if (!string.IsNullOrWhiteSpace(this.CategoryName) && this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/categories/category/{this.CategoryName}/{this.model.Category}/page/{this.model.Page}");
            }
            else if (string.IsNullOrWhiteSpace(this.model.Query) && string.IsNullOrWhiteSpace(this.CategoryName) && !this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/categories/page/{this.model.Page}");
            }
        }

    }
}
