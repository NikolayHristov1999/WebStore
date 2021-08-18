namespace WebStore.Web.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Data.Models;

    using static WebStore.Data.Common.DataConstants;

    public static class Dealers
    {
        public static IEnumerable<Dealer> GetFiveDealers()
        {
            var dealers = new List<Dealer>();
            for (int i = 0; i < 5; i++)
            {
                dealers.Add(new Dealer
                {
                    Id = i.ToString(),
                    UserId = i.ToString(),
                });
            }

            return dealers;
        }
    }
}
