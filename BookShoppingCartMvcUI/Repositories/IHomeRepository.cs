namespace CloudPart3
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int styleId = 0);
        Task<IEnumerable<Style>> Styles();
    }
}