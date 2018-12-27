namespace BillPaymentValidator.Models
{
	public class BillInformation
	{
		public BillType Type { get; set; }
		public string PaymentPerid { get; set; }
		public string Year { get; set; }
		public string Amount { get; set; }
		public string BillId { get; set; }
		public string PaymentId { get; set; }
	}

	public enum BillType : int
	{
		Unknown = 0,
		Water = 1,
		Power = 2,
		Gas = 3,
		Phone = 4,
		Mobile = 5,
		MunicipalityTax = 6,
		Tax = 8,
		PoliceFine = 9
	}
}
