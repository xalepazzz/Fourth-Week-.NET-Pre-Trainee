namespace DatabaseLayer.Models
{
    public record Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
