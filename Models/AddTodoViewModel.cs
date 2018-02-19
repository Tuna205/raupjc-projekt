using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak2.Models
{
    public class AddTodoViewModel
    {
		[Required(ErrorMessage = "Please input text")]
		public string Text { get; set; }

		
		public DateTime? DateDue { get; set; }

		public string Labele { get; set; }

    }
}
