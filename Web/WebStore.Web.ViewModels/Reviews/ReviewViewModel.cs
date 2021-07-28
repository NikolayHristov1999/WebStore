namespace WebStore.Web.ViewModels.Reviews
{
    using System;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class ReviewViewModel : IMapFrom<Review>
    {
        public string Name { get; set; }

        public byte Stars { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
