// ReSharper disable VirtualMemberCallInConstructor
namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;

    using WebStore.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Contacts = new HashSet<Contact>();
            this.Orders = new HashSet<Order>();
            this.Carts = new HashSet<Cart>();
            this.Reviews = new HashSet<Review>();
        }

        public ApplicationUser(string name)
            :this()
        {
            this.UserName = name;
        }

        [DataType("Date")]
        public DateTime? DateOfBirth { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
