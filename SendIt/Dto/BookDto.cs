namespace SendIt.Dto
{
    public class BookDto
    {
        public long Id { get; set; }
        public string Boo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }




    public class AddBookDto
    {
      
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
    }
}
