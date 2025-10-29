namespace DatabaseLayer.Models
{
    public record Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly PublishDate  { get; set; }
        public Author Author { get; set; }
        public int AuthorId {  get; set; } 
        

    }
}
