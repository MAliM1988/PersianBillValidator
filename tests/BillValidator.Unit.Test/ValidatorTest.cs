using FluentAssertions;
using System;
using BillPaymentValidator.Exceptions;
using Xunit;

namespace BillPaymentValidator.Unit.Test
{
	public class ValidatorTest : IDisposable
	{
		#region Fields

		private BillValidator _billValidator;

		#endregion Fields

		#region Ctor

		public ValidatorTest()
		{
			_billValidator = new BillValidator();
		}

		#endregion Ctor

		#region Test Members

		[Fact]
		public void BillId_validation_should_throw_exception_when_bilId_is_null_or_empty()
		{
			Action nullstringAction = () => _billValidator.ValidateBill("","");
			Action nullAction = () => _billValidator.ValidateBill(null,null);

			nullstringAction.Should().Throw<NullReferenceException>().WithMessage("billId is null or empty");
			nullAction.Should().Throw<NullReferenceException>().WithMessage("billId is null or empty");
		}

		[Fact]
		public void BillId_validation_should_throw_exception_when_lenght_of_billId_less_than_06()
		{
			Action action = () => _billValidator.ValidateBill("12345","12345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("billId lenght is less than 6");
		}

		[Fact]
		public void BillId_validation_should_throw_exception_when_lenght_of_billId_more_than_13()
		{
			Action action = () => _billValidator.ValidateBill("12345678912345","12345678912345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("billId lenght is more than 13");
		}

		[Fact]
		public void BillId_validation_should_be_return_false_when_billId_is_invalid()
		{
			var validationResult = _billValidator.ValidateBill("1234567891","2510006");

			validationResult.Should().BeFalse();
		}

		[Fact]
		public void BillId_validation_should_be_return_true_when_billId_is_valid()
		{
			var validationResult = _billValidator.ValidateBill("1677036253","00025100064");

			validationResult.Should().BeTrue();
		}

		[Fact]
		public void PaymentId_validation_should_throw_exception_when_bilId_is_null_or_empty()
		{
			Action nullstringAction = () => _billValidator.ValidateBill("001234567892","");
			Action nullAction = () => _billValidator.ValidateBill("001234567892",null);

			nullstringAction.Should().Throw<NullReferenceException>().WithMessage("paymentId is null or empty");
			nullAction.Should().Throw<NullReferenceException>().WithMessage("paymentId is null or empty");
		}

		[Fact]
		public void PaymentId_validation_should_throw_exception_when_lenght_of_billId_less_than_06()
		{
			Action action = () => _billValidator.ValidateBill("001234567892","12345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("paymentId lenght is less than 6");
		}

		[Fact]
		public void PaymentId_validation_should_throw_exception_when_lenght_of_billId_more_than_13()
		{
			Action action = () => _billValidator.ValidateBill("001234567892","12345678912345");

			action.Should().Throw<InvalidLenghtException>().WithMessage("paymentId lenght is more than 13");
		}

		[Fact]
		public void PaymentId_validation_should_be_return_false_when_billId_is_invalid()
		{
			var validationResult = _billValidator.ValidateBill("1234567891","00025100068");

			validationResult.Should().BeFalse();
		}

		[Fact]
		public void PaymentId_validation_should_be_return_true_when_billId_is_valid()
		{
			var validationResult = _billValidator.ValidateBill("5562358230152", "43976730");

			validationResult.Should().BeTrue();
		}

		#endregion Test Members

		#region IDisposable Members

		public void Dispose()
		{
			_billValidator?.Dispose();
			_billValidator = null;

			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Members
	}
}