namespace WebStore.Web.ViewModels.Reviews
{
    using System.Collections.Generic;
    using System.Linq;

    public class ListReviewsViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; }

        public double AvrScore => this.Reviews.Sum(x => x.Stars) / (double)this.Reviews.Count();
    }
}
