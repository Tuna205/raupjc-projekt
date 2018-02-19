using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Zadatak2.ProsliZadatak;

namespace Zadatak2.Models
{
    public class TodoViewModel
    {
	    public Guid _Id;
		
	    public String Text;

	    public DateTime? DateDue;

	    public bool Completed;

	    public DateTime DateCompleted;

	    public List<TodoItemLabel> Labele;

	    private readonly ITodoRepository _repository;
	    private readonly String _userId;

		public TodoViewModel(TodoItem todoItem, ITodoRepository repository, String userId)//mozda dodati i usera
	    {
		    _Id = todoItem.Id;
		    Text = todoItem.Text;
		    DateDue = todoItem.DateDue;
		    Completed = todoItem.Completed;
		    DateCompleted = todoItem.DateCreated;
		    _repository = repository;
		    _userId = userId;
		    Labele = todoItem.Labels;

	    }

		public string GetDaysLeft()
		{

			if (DateDue != null)
			{
				int daysLeft = DateDue.Value.Day - DateTime.Now.Day;

				if (daysLeft < 0) return "(Rok je prošao!)";
				else
				switch (daysLeft)
				{
					case 0: return "(Danas!)";
					case 1: return "(Za jedan dan!)";
					case 2: return "(Za dva dana!)";
					case 3: return "(Za tri dana!)";
					case 4: return "(Za četiri dana!)";
					case 5: return "(Za pet dana!)";
					case 6: return "(Za šest dana!)";
					case 7: return "(Za sedam dana!)";
					case 8: return "(Za osam dana!)";
					case 9: return "(Za devet dana!)";
					default:
						return "(Više od deset dana)";
				}
			}
			return "(Rok nije zadan)";
		}

    }
}
