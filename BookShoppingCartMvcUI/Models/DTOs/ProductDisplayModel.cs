namespace CloudPart3.Models.DTOs
{
    public class ProductDisplayModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Style> Styles { get; set; }
        public string STerm { get; set; } = "";
        public int StyleId { get; set; } = 0;
    }
}
