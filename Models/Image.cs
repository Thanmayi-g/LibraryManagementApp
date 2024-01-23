using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookAPI.Models
{
    public class Image
    {
       
            [Key]
            public int ImageId { get; set; }

            public string Url { get; set; }

            [ForeignKey("Book")]
            public int BookId { get; set; }

            public Book Book { get; set; }
        
    }
}
