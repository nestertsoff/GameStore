namespace GameStore.Web.Areas.Payment.Models.PaymentTypes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using GameStore.Web.Resouces;

    public class VisaViewModel
    {
        public string CardHoldersName { get; set; }

        public long CardNumber { get; set; }

        public DateTime DateOfExpity { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "CardVerificationValue")]
        public int CardVerificationValue { get; set; }
    }
}