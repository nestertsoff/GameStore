namespace GameStore.Web.Areas.Payment.Models.PaymentTypes
{
    public class IBoxViewModel
    {
        public int AccountNumber { get; set; }

        public int InvoiceNumber { get; set; }

        public decimal Sum { get; set; }
    }
}