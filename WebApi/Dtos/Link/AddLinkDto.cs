namespace WebApi.Dtos.Link
{
    public class AddLinkDto
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}