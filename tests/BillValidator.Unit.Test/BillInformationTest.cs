using BillPaymentValidator.Exceptions;
using BillPaymentValidator.Models;
using FluentAssertions;
using System;
using Xunit;

namespace BillPaymentValidator.Unit.Test
{
	public class BillInformationTest : IDisposable
	{
		#region Fields

		private BillValidator _billValidator;

		#endregion Fields

		#region Ctor

		public BillInformationTest()
		{
			_billValidator = new BillValidator();
		}

		#endregion Ctor

		#region Test Members

		[Fact]
		public void BillId_data_extractor_should_throw_exception_when_billId_is_null_or_empty()
		{
			Action nullstringAction = () => _billValidator.GetBillInformation("", "");
			Action nullAction = () => _billValidator.GetBillInformation(null, null);

			nullstringAction.Should().Throw<NullReferenceException>().WithMessage("billId is null or empty");
			nullAction.Should().Throw<NullReferenceException>().WithMessage("billId is null or empty");
		}

		[Fact]
		public void BillId_data_extractor_should_throw_exception_when_lenght_of_billId_less_than_06()
		{
			Action action = () => _billValidator.GetBillInformation("12345", "12345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("billId lenght is less than 6");
		}

		[Fact]
		public void BillId_data_extractor_should_throw_exception_when_lenght_of_billId_more_than_13()
		{
			Action action = () => _billValidator.GetBillInformation("12345678912345", "12345678912345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("billId lenght is more than 13");
		}

		[Fact]
		public void BillId_data_extractor_should_when_not_found_bill_type_return_billInformation_with_unknown_type()
		{
			var result = _billValidator.GetBillInformation("1677036203", "00025100064");

			result.Type.Should().Be(BillType.Unknown);
		}

		[Fact]
		public void BillId_data_extractor_should_return_billInformation_and_extract_bill_type()
		{
			var result = _billValidator.GetBillInformation("1677036253", "00025100064");

			result.Type.Should().Be(BillType.Mobile);
		}

		[Fact]
		public void PaymentId_data_extractor_should_throw_exception_when_bilId_is_null_or_empty()
		{
			Action nullstringAction = () => _billValidator.GetBillInformation("001234567892", "");
			Action nullAction = () => _billValidator.GetBillInformation("001234567892", null);

			nullstringAction.Should().Throw<NullReferenceException>().WithMessage("paymentId is null or empty");
			nullAction.Should().Throw<NullReferenceException>().WithMessage("paymentId is null or empty");
		}

		[Fact]
		public void PaymentId_data_extractor_should_throw_exception_when_lenght_of_billId_less_than_06()
		{
			Action action = () => _billValidator.GetBillInformation("001234567892", "12345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("paymentId lenght is less than 6");
		}

		[Fact]
		public void PaymentId_data_extractor_should_throw_exception_when_lenght_of_billId_more_than_13()
		{
			Action action = () => _billValidator.GetBillInformation("5562358230152", "43976730123654654654");

			action.Should().Throw<InvalidLenghtException>().WithMessage("paymentId lenght is more than 13");
		}

		[Fact]
		public void PaymentId_data_extractor_should_return_billInformation_with_period_code()
		{
			var result = _billValidator.GetBillInformation("5562358230152", "43976730");

			result.PaymentPerid.Should().Be("67");
		}

		[Fact]
		public void PaymentId_data_extractor_should_return_billInformation_with_bill_year()
		{
			var result = _billValidator.GetBillInformation("5562358230152", "43976730");

			result.Year.Should().Be("1397");
		}

		[Fact]
		public void PaymentId_data_extractor_should_return_billInformation_with_bill_amount()
		{
			var result = _billValidator.GetBillInformation("5562358230152", "43976730");

			result.Amount.Should().Be("439000");
		}

		[Fact]
		public void GetBillInformation_should_return_billInformation_with_billId()
		{
			var result = _billValidator.GetBillInformation("5562358230152", "43976730");

			result.BillId.Should().Be("5562358230152");
		}

		[Fact]

		public void GetBillInformation_should_return_billInformation_with_paymentId()
		{
			var result = _billValidator.GetBillInformation("5562358230152", "43976730");

			result.PaymentId.Should().Be("43976730");
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			_billValidator?.Dispose();
			_billValidator = null;

			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
