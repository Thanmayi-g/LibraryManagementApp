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
        [StringLength(200, ErrorMessage = "BookName cannot exceed 200 characters.")]
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


        // Multiple image URLs
        public List<Image> Images { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }










    }

}



