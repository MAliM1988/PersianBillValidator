using BillPaymentValidator.Exceptions;
using BillPaymentValidator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BillPaymentValidator
{
	public class BillValidator : IDisposable
	{
		#region BillValidator Members

		public bool ValidateBill(string billId, string paymentId)
		{
			#region BillId Validation

			if (string.IsNullOrEmpty(billId)) throw new NullReferenceException($"{nameof(billId)} is null or empty");
			if (billId.Length < 6) throw new InvalidLenghtException($"{nameof(billId)} lenght is less than 6");
			if (billId.Length > 13) throw new InvalidLenghtException($"{nameof(billId)} lenght is more than 13");

			var billIdValidateBit = CalculateValidateBit(billId.TrimStart('0'), false);

			#endregion

			#region PaymentId Validation

			if (string.IsNullOrEmpty(paymentId)) throw new NullReferenceException($"{nameof(paymentId)} is null or empty");
			if (paymentId.Length < 6) throw new InvalidLenghtException($"{nameof(paymentId)} lenght is less than 6");
			if (paymentId.Length > 13) throw new InvalidLenghtException($"{nameof(paymentId)} lenght is more than 13");

			var paymentIdFirstValidateBit = CalculateValidateBit(paymentId.TrimStart('0'), true);
			var paymentIdSecondValidateBit =
				CalculateValidateBit(String.Concat(billId.TrimStart('0'), paymentId.TrimStart('0')), false);

			#endregion

			return billId.EndsWith(billIdValidateBit) &&
				   paymentId.EndsWith(String.Concat(paymentIdFirstValidateBit.ToString(),
					   paymentIdSecondValidateBit.ToString()));
		}

		public BillInformation GetBillInformation(string billId, string paymentId)
		{
			BillInformation information = new BillInformation();

			if (string.IsNullOrEmpty(billId)) throw new NullReferenceException($"{nameof(billId)} is null or empty");
			if (billId.Length < 6) throw new InvalidLenghtException($"{nameof(billId)} lenght is less than 6");
			if (billId.Length > 13) throw new InvalidLenghtException($"{nameof(billId)} lenght is more than 13");

			if (string.IsNullOrEmpty(paymentId)) throw new NullReferenceException($"{nameof(paymentId)} is null or empty");
			if (paymentId.Length < 6) throw new InvalidLenghtException($"{nameof(paymentId)} lenght is less than 6");
			if (paymentId.Length > 13) throw new InvalidLenghtException($"{nameof(paymentId)} lenght is more than 13");

			information.Type = ExtractBillType(billId);
			information.PaymentPerid = ExtractPaymentPerid(paymentId);
			information.Year = ExtractYear(paymentId);
			information.Amount = ExtractAmount(paymentId);
			information.BillId = billId;
			information.PaymentId = paymentId;

			return information;
		}

		#endregion BillValidator Members

		#region Private Members

		private string CalculateValidateBit(string value, bool isSecondBit)
		{
			var multiplierFactors = new List<int>() {
				2,3,4,5,6,7,2,3,4,5,6,7,2,3,4,5,6,7,2,3,4,5,6,7,2,3
			};
			var reverseValue = value.Reverse().ToList();
			reverseValue.RemoveAt(0);
			if (isSecondBit) reverseValue.RemoveAt(0);

			var sumOfValues = reverseValue.Select((t, i) => int.Parse(t.ToString()) * multiplierFactors[i]).Sum();
			var base11Result = sumOfValues % 11;

			return base11Result == 0 || base11Result == 1 ? "0" : (11 - base11Result).ToString();
		}

		private BillType ExtractBillType(string billId)
		{
			var reverseValue = billId.Reverse().ToList();
			reverseValue.RemoveAt(0);

			return (BillType)Enum.Parse(typeof(BillType), reverseValue[0].ToString());
		}

		private string ExtractPaymentPerid(string paymentId)
		{
			var reverseValue = paymentId.Reverse().ToList();
			reverseValue.RemoveAt(0);
			reverseValue.RemoveAt(0);

			return string.Concat(reverseValue[1], reverseValue[0]);
		}

		private string ExtractYear(string paymentId)
		{
			#region Convert Date

			var currentDate = DateTime.UtcNow;
			PersianCalendar persianCalendar = new PersianCalendar();

			var persianYear = persianCalendar.GetYear(currentDate).ToString();

			#endregion

			var reverseValue = paymentId.Reverse().ToList();
			reverseValue.RemoveRange(0, 4);

			return string.Concat(persianYear.Remove(persianYear.Length - 1), reverseValue[0]);
		}

		private string ExtractAmount(string paymentId)
		{
			var reverseValue = paymentId.Reverse().ToList();
			reverseValue.RemoveRange(0, 5);
			var reverseAmount = string.Concat(reverseValue);

			var amount = int.Parse(string.Concat(reverseAmount.Reverse()))*1000;

			return amount.ToString();
		}

		#endregion Private Members

		#region IDisposable Members

		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Members
	}
}