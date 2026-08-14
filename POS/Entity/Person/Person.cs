namespace POS.Entity.Person
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Supplier? Supplier { get; set; }
        public Buyer? Buyer { get; set; }
        public Employee? Employee { get; set; }
        public User? User { get; set; }

    }
}
