namespace Data.Models
{
    public partial class PersonConnection
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string ConnectionType { get; set; }
        public int ConnectedPersonId { get; set; }

        public virtual Person ConnectedPerson { get; set; }
        public virtual Person Person { get; set; }
    }
}
