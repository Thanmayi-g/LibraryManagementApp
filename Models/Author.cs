using System.ComponentModel.DataAnnotations;

namespace BookAPI.Models
{
    public class Author
    {
        [Key]

        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
