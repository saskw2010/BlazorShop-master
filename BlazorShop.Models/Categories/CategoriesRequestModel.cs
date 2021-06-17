namespace BlazorShop.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static ErrorMessages;
    using static Data.ModelConstants.Common;
    using static Data.ModelConstants.category;
    public class CategoriesRequestModel
    {
        [Required]
        [StringLength(
            MaxNameLength, 
            ErrorMessage = StringLengthErrorMessage, 
            MinimumLength = MinNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxUrlLength)]
        public string ImageSource { get; set; }
    }
}
