namespace WebStore.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewFormModel
    {
        [MinLength(3)]
        [Required]
        [Display(Name="Your Full Name")]
        public string FullName { get; set; }

        [MinLength(5)]
        [Required]
        [Display(Name = "Review Content")]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public byte Stars { get; set; }
    }
}
