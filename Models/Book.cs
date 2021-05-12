namespace MayBookList.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public bool? Finished { get; set; }
        public bool? Abandoned { get; set; }
        public bool? FromLibrary { get; set; }
        public bool? Owned { get; set; }
        public bool? GivenAway { get; set; }
    }
}