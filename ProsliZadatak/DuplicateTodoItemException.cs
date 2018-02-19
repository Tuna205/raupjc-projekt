using System;

namespace Zadatak2.ProsliZadatak
{
	public class DuplicateTodoItemException : Exception
	{
		public DuplicateTodoItemException()
		{
			
		}
		public DuplicateTodoItemException(string msg) : base(msg)
		{

		}
	}
}