using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [Timestamp]
        public byte[] CreatedAt { get; set; }
    }
}