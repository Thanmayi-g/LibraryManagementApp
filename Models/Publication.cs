using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BookAPI.Models
{
    public class Publication
    {
        [Key]
        public int PublicationId { get; set; }
        public string PublishingCompanyName { get; set; }

        [Display(Name = "Address")]
        [ForeignKey("Address")]

        public virtual int AddressId { get; set; }

       
       public virtual Address Address { get; set; }
       public virtual ICollection<Book> Books { get; set; }
    }
   }

