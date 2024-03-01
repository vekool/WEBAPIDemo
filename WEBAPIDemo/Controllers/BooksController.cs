using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEBAPIDemo.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WEBAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>()
        {
            new Book{BookISBN=837388, Title="Some Title", Price=345.67},
            new Book{BookISBN=345658, Title="Some Other Title", Price=64.27},
            new Book{BookISBN=838376, Title="Some New Title", Price=486.75}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> AllBooks()
        {
            return books;
        }
        /// <summary>
        /// Gets a book by it's ISBN Number
        /// </summary>
        /// <param name="BookISBN">the book to fetch</param>
        /// <returns>NoContent if ISBN not provided, NotFound if boks does not exist, The book itself</returns>
        [HttpGet("{BookISBN}")]
        public ActionResult<Book> GetBook(int? BookISBN)
        {
            if (BookISBN.HasValue == false)
            {
                return NoContent();
            }
            var book = books.FirstOrDefault(b => b.BookISBN == BookISBN.Value);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        [HttpPost]
        public ActionResult<Book> InsertBook(Book book)
        {
            books.Add(book);
            return CreatedAtAction("GetBook", new { BookISBN = book.BookISBN }, book);
            //201 - SOmething was created on server
            //api/GetBook/8373837
            //I am also sending the new book that was created
        }
        /// <summary>
        /// Deletes a book by it's ISBN Number
        /// </summary>
        /// <param name="BookISBN">the book to fetch</param>
        /// <returns>NoContent if ISBN not provided, NotFound if boks does not exist, The book itself</returns>
        [HttpDelete("{BookISBN}")]
        public ActionResult<Book> DeleteBook(int? BookISBN)
        {
            if (BookISBN.HasValue == false)
            {
                return NoContent();
            }
            var book = books.FirstOrDefault(b => b.BookISBN == BookISBN.Value);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            return NoContent();
        }
        [HttpPut("{BookISBN}")]
        public ActionResult<Book> UpdateBook(int? BookISBN, Book book)
        {
            if (BookISBN.HasValue == false)
            {
                return NoContent();
            }
            var oldbook = books.FirstOrDefault(x => x.BookISBN == BookISBN);
            if (oldbook == null)
            {
                return NotFound();
            }
            oldbook.BookISBN = book.BookISBN;
            oldbook.Price = book.Price;
            oldbook.Title = book.Title;
            //db.SaveChanges()
            return NoContent();

            /*POST - Create
           * GET - Read
           * PUT - UPdate
           * Delete - Delete
           */

        }
    }
    
}
