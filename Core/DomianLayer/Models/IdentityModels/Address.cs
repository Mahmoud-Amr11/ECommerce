using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.IdentityModels
{
    public class Address 
    {
        public int Id { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
