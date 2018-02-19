using System;

namespace Zadatak2.ProsliZadatak
{
	public class TodoAccessDeniedException : Exception
	{
		public TodoAccessDeniedException()
		{
			
		}

		public TodoAccessDeniedException(string message) : base(message)
		{

		}
	}
}
