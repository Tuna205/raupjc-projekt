using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Zadatak2.ProsliZadatak;

namespace Zadatak2.Models
{
    public class CompletedViewModel
    {
	    private readonly List<TodoViewModel> listaObjekata;

		    public string Username { get; set; }

		    public bool IsEmailConfirmed { get; set; }

		    [Required]
		    [EmailAddress]
		    public string Email { get; set; }

		    [Phone]
		    [Display(Name = "Phone number")]
		    public string PhoneNumber { get; set; }

		    public string StatusMessage { get; set; }

		    private readonly Guid _userGuid = Guid.Empty;

		    //private readonly String _userId;

		    private readonly ITodoRepository _repository;
		    //private readonly ApplicationUser _user;
		    private readonly String _userId;

		    public CompletedViewModel()
		    {

		    }

		    public CompletedViewModel(ITodoRepository repository, String userId)
		    {

			    _userId = userId;
			    //var user = userManager.Users
			    //_userId = userId; 
			    _repository = repository;
			    listaObjekata = new List<TodoViewModel>();
		    }

		    public List<TodoViewModel> GetRepos()
		    {
			    List<TodoItem> todos = _repository.GetCompleted(_userId); //Pazi koji repo dodajes
			    if (todos != null)
				    foreach (var todoItem in todos)
				    {
					    List<TodoItemLabel> til = _repository.GetLabels(todoItem);
						TodoViewModel todoView = new TodoViewModel(todoItem, _repository, _userId);
						todoView.Labele = til;
						listaObjekata.Add(todoView);
				    }
			    return listaObjekata;
		    }
	}
}
