using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkPermitSystem.Models.DomainModels
{
    public class User : IdentityUser
    {
        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [NotMapped]
        public IList<string>? RoleNames { get; set; }
    }
}