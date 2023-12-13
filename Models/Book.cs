using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        public DateTime PublishedOn { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Genre { get; set; }

    

        [Display(Name = "PublicationId")]
        [ForeignKey("Publication")]
        public virtual int PublicationId { get; set; }

        public virtual Publication Publication { get; set; }


        [Display(Name = "Author")]
        [ForeignKey("Author")]
        public virtual int AuthorId { get; set; }

        public virtual Author Author { get; set; }











    }

}



