namespace Expensier.Domain.Models
{
    public class HistoricalData
    {
        public required DateTime Date { get; set; }
        public required double Price { get; set; }
    }
}
