namespace Data.Models
{
    public partial class Phone
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }

        public virtual Person Person { get; set; }
    }
}
