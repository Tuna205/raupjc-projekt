using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zadatak2.Models;
using Zadatak2.ProsliZadatak;
using System.Web;
using Microsoft.AspNetCore.Http;
using Zadatak2.Services;

namespace Zadatak2.Controllers
{

	[Authorize]
	public class HomeController : Controller
	{
		//private ApplicationUser _user;

		//private readonly Task<ApplicationUser> asyncUser;

		private readonly string _userId;

		private readonly ITodoRepository _repository;

		public HomeController(ITodoRepository repository, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
		{
			_repository = repository;
			//while(HttpContext == null) {HttpContext = HttpContext}
			_userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			//_userId = userManager.GetUserId(HttpContext.User);
		}

		public IActionResult Index()
		{

			IndexViewModel indexModel = new IndexViewModel(_repository, _userId);
				
			var repo = indexModel.GetRepos();
			return View(repo);
		}


		[HttpPost]
		public IActionResult Add(AddTodoViewModel addTodoView) //async
		{
			if (ModelState.IsValid)
			{
				
				TodoItem item = new TodoItem(addTodoView.Text, _userId, addTodoView.DateDue);
				LabelManager lm = new LabelManager(item, addTodoView.Labele,_repository);
				TodoItem temp = lm.GetItemWithLabels();
				_repository.Add(temp);
				
				return RedirectToAction("Index");
			}

			return View(addTodoView);

		}
		
		public IActionResult Add() //async
		{
			return View();
		}
		


		public IActionResult Completed() //async
		{
			CompletedViewModel compModel = new CompletedViewModel(_repository, _userId);

			var repo = compModel.GetRepos();

			return View(repo);

		}

		public IActionResult Error()
		{
			return View();
		}


		public IActionResult MarkAsCompleted(Guid itemId)
		{
			_repository.MarkAsCompleted(itemId, _userId);
			return RedirectToAction("Index");
		}


		public IActionResult RemoveFromCompleted(Guid itemId)
		{
			_repository.Remove(itemId, _userId);
			return RedirectToAction("Completed");
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}
	}
}
