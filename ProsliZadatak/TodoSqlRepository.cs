using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Zadatak2.ProsliZadatak
{
	public class TodoSqlRepository : ITodoRepository
	{
		private ImmutableHashSet<string> _setLabela;

		private readonly TodoDbContext _context;
		public TodoSqlRepository(TodoDbContext context)
		{
			_context = context;
			_setLabela = GetAllLabels();
		}
		
		public TodoItem Get(Guid todoId, String userId)
		{
			TodoItem foundItem = _context.TodoItemSet.Find(todoId);
			
			if(foundItem == null) return null;
			if (foundItem.UserId == userId)
				return foundItem;
			
			throw new TodoAccessDeniedException("The user doesn't own this item");
			
		}

		public void Add(TodoItem todoItem)
		{
			Guid newItemId = todoItem.Id;
			if (_context.TodoItemSet.Find(newItemId) == null)
			{
				_context.TodoItemSet.Add(todoItem);
				
				foreach (TodoItemLabel itemLabel in todoItem.Labels)
				{

					itemLabel.LabelTodoItems.Add(todoItem);

					if (_setLabela.Contains(itemLabel.Value))
					{
						var labela = _context.TodoItemLabelSet.Find(itemLabel);
						labela.LabelTodoItems.Add(todoItem);
					}
					else
						_context.TodoItemLabelSet.Add(itemLabel);
					_setLabela.Add(itemLabel.Value);
				}
				_context.SaveChanges();
			}
			else throw new DuplicateTodoItemException("duplicate id: " + newItemId);

		}

		public bool Remove(Guid todoId, String userId)
		{
			TodoItem foundItem = _context.TodoItemSet.Find(todoId);

			if (foundItem == null) return false;
			if (foundItem.UserId == userId)
			{
				if(foundItem.Labels != null)
					foreach (var foundItemLabel in foundItem.Labels)
					{
						var nes =_context.TodoItemLabelSet.Find(foundItemLabel).LabelTodoItems;
						nes.Remove(foundItem);
						if (nes.Count == 0) _context.TodoItemLabelSet.Remove(foundItemLabel);
					}
				_context.TodoItemSet.Remove(foundItem);
				_context.SaveChanges();
				return true;
			}
			throw new TodoAccessDeniedException("The user doesn't own this item");
		}

		public void Update(TodoItem todoItem, String userId)
		{
			Guid itemId = todoItem.Id;
			TodoItem foundItem = _context.TodoItemSet.Find(itemId);
			if (foundItem == null) Add(todoItem);
			else if (!userId.Equals(foundItem.UserId)) throw new TodoAccessDeniedException("The user doesn't own this item");
		}

		public bool MarkAsCompleted(Guid todoId, String userId)
		{
			TodoItem foundItem = _context.TodoItemSet.Find(todoId);
			if (foundItem == null) return false;
			if (userId.Equals(foundItem.UserId))
			{
				foundItem.Completed = true;
				foundItem.DateCompleted = DateTime.Now;
				_context.SaveChanges();
				return true;
			}
			throw new TodoAccessDeniedException("The user doesn't own this item");
		}

		public List<TodoItem> GetAll(String userId)
		{
			List<TodoItem> list = _context.TodoItemSet  .Where(ti => ti.UserId.Equals(userId))
														.OrderByDescending(ti => ti.DateCreated)
														.ToList();

			return list;
		}

		public List<TodoItem> GetActive(String userId)
		{
			List<TodoItem> list = _context.TodoItemSet  .Where(ti => ti.UserId.Equals(userId))
														.Where(ti => !ti.Completed)
														.ToList();

			return list;
		}

		public List<TodoItem> GetCompleted(String userId)
		{
			List<TodoItem> list = _context  .TodoItemSet.Where(ti => ti.UserId.Equals(userId))
											.Where(ti => ti.Completed)
											.ToList();

			return list;
		}

		public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, String userId)
		{
			List<TodoItem> list = _context.TodoItemSet  .Where(ti => ti.UserId.Equals(userId))
														.AsEnumerable()
														.Where(filterFunction)
														.ToList();

			return list;
		}

		public ImmutableHashSet<string> GetAllLabels()
		{
			return _context.TodoItemLabelSet.Select(til => til.Value).ToImmutableHashSet();
		}

		public List<TodoItemLabel> GetLabels(TodoItem item)
		{
			var temp = _context.TodoItemSet.Where(ti => ti.Id == item.Id).SelectMany(ti => ti.Labels).ToList();
			return temp;
		}
	}


	/*
	class Test
	{
		public static void Main(string[] args)
		{
			string ConnectionString = "theConnString1";
			using (var db = new TodoDbContext(ConnectionString))
			{
				Guid guid = Guid.NewGuid();
				TodoItem item = new TodoItem("blabal", guid);
				TodoSqlRepository repo = new TodoSqlRepository(db);
				repo.Add(item);
				repo.Get(guid, guid);
				Console.WriteLine("sve ok");
			}
		}
	}
	*/
}
