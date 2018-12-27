using System;

namespace BillPaymentValidator.Exceptions
{
	public class InvalidLenghtException : Exception
	{
		public InvalidLenghtException(string message) : base(message)
		{
		}
	}
}