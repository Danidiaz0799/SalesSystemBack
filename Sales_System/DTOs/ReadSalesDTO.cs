namespace SalesSystem.Sales.DTOs
{
    public class ReadSalesDTO
    {
        public int SaleId { get; set; }

        public int AdvisorId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; } = null!;

        public int Price { get; set; }

        public int SaleStatusId { get; set; }

        public int CustomerId { get; set; }
    }
}
