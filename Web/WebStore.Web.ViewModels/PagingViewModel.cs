
namespace WebStore.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class PagingViewModel
    {
        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ProductsPerPage);

        public int ProductsCount { get; set; }

        public int ProductsPerPage { get; set; }

        public Dictionary<string, string> QueryParams { get; set; } = new Dictionary<string, string>();
    }
}
