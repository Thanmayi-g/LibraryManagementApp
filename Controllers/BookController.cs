using BookAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class BookController : ControllerBase
    {
        BookContext db = null;
        public BookController(BookContext context)

        {
            db = context;
        }

        
        [HttpGet]
        public IActionResult GetAllBooks(int page = 1, int pageSize = 10)
        {
            var query = from book in db.Books
                        join author in db.Authors on book.AuthorId equals author.AuthorId
                        join publisher in db.Publications on book.PublicationId equals publisher.PublicationId
                        orderby book.PublishedOn ascending
                        select new
                        {
                            book.BookName,
                            book.PublishedOn,
                            book.Language,
                            book.Genre,
                            author.FirstName,
                            author.LastName,
                            publisher.PublishingCompanyName,
                            // book.ImageUrl
                            Images = book.Images.Select(image => image.Url).ToList(),
                            book.Description
                        }; 

            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = pagedData
            });
        }


        [HttpGet]

        [Route("{id}")]
        public IActionResult GetBookbyId(int id)
        {
            var query = from books in db.Books
                        join author in db.Authors
                        on books.AuthorId equals author.AuthorId
                        join publisher in db.Publications
                        on books.PublicationId equals publisher.PublicationId
                        join boo in db.Books
                        on id equals boo.BookId
                        select new
                        {
                            books.BookId,
                            books.BookName,
                            books.PublishedOn,
                            books.Language,
                            books.Genre,
                            author.FirstName,
                            author.LastName,
                            publisher.PublishingCompanyName
                        };
           
            var book = query.Where(x => x.BookId == id).ToList();
            return Ok(book);
           
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult AddBook([FromBody] BookInputModel bookobj)
        {
            if (bookobj == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Book book = new Book();

                book.BookName = bookobj.BookName;
                book.PublishedOn = bookobj.PublishedOn;
                book.Language = bookobj.Language;
                book.Genre = bookobj.Genre;
                book.PublicationId = bookobj.PublicationId;
                book.AuthorId = bookobj.AuthorId;
                //     book.ImageUrls = bookobj.ImageUrl;
                db.Books.Add(book);
                db.SaveChanges();
                return Ok("Book Added Successfully");
            }
            return BadRequest("Book Not Added");


        }


        [HttpGet]
        [Route("GetBook/{name}")]
        public IActionResult GetBookByName(string name)
        {
            var booksearch = db.Books.SingleOrDefault(x => x.BookName == name);

            if (booksearch == null)
            {
                return NotFound($"Book with name '{name}' not found.");
            }
            var query = from book in db.Books where book.BookName.Equals(booksearch.BookName)
                        join author in db.Authors on book.AuthorId equals author.AuthorId
                        join publisher in db.Publications on book.PublicationId equals publisher.PublicationId
                        select new
                        {
                            book.BookId,
                            book.BookName,
                            book.PublishedOn,
                            book.Language,
                            book.Genre,
                            author.FirstName,
                            author.LastName,
                            publisher.PublishingCompanyName
                        };
            return Ok(query);
            
        }

        [HttpPut]
        [Route("{name}")]
        public IActionResult EditBook(string name,[FromBody] BookEditModel bookobj)
        {
            if (bookobj == null)
            {
                return BadRequest("Invalid request data.");
            }

            var book = db.Books.SingleOrDefault(x => x.BookName == name);

            if (book == null)
            {
                return NotFound($"Book with name '{name}' not found.");
            }

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            book.BookName=bookobj.BookName;
            book.PublishedOn = bookobj.PublishedOn;
            book.Language = bookobj.Language;
            book.Genre = bookobj.Genre;
            book.PublicationId = bookobj.PublicationId;
            book.AuthorId = bookobj.AuthorId;

            try
            {
                db.SaveChanges();

                return Ok(name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the book: {ex.Message}");
            }
        }


        [HttpDelete]
       public IActionResult DeleteBook(string name)
        {
            var book = (from x in db.Books
                        where x.BookName == name
                        select x).SingleOrDefault();
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            db.SaveChanges();
            return Ok("Book Deleted Successfully");
        }

    }
    }
public class BookInputModel
{
    public string BookName { get; set; }

    public string Language { get; set; }

    public string Genre { get; set; }

    public DateTime PublishedOn { get; set; }

    public int PublicationId { get; set; }

    public int AuthorId { get; set; }
    //public string? ImageUrl { get; set; }
    
}

public class BookEditModel
{
   
    public string BookName { get; set; }
    public string Language { get; set; }

    public string Genre { get; set; }

    public DateTime PublishedOn { get; set; }

    public int PublicationId { get; set; }

    public int AuthorId { get; set; }
}
//https://sportshub.cbsistatic.com/i/2022/06/10/4070adc6-36b5-41c8-8266-995f068c8180/english-harry-potter-2-epub-9781781100226.jpg?auto=webp&width=200&height=200&crop=0.667:1,smart" 