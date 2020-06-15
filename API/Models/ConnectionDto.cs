namespace API.Models
{
    public class ConnectionDto
    {
        public string ConnectionType { get; set; }
        public int ConnectedPersonId { get; set; }

        public virtual ConnectedPersonDto ConnectedPerson { get; set; }
    }
}