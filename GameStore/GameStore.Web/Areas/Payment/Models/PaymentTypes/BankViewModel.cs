namespace GameStore.Web.Areas.Payment.Models.PaymentTypes
{
    using System;

    public class BankViewModel
    {
        public DateTime InvoiceDate { get; set; }

        public int InvoiceNumber { get; set; }

        public string ClientName { get; set; }

        public string ClientId { get; set; }

        public string ClientAdress { get; set; }

        public string ClientPhone { get; set; }

        public string ClientEmail { get; set; }

        public string ContractorName { get; set; }

        public string ContractorAdress { get; set; }

        public string ContractorPhone { get; set; }

        public string ContractorEmail { get; set; }

        public OrderViewModel Order { get; set; }
    }
}