using System;
using System.Collections.Generic;

namespace Zadatak2.ProsliZadatak
{
	public class TodoItem
	{
		// nisam 100% jel treba
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateCompleted { get; set; }
		public bool Completed { get; set; }

		/// <summary >
		/// User id that owns this TodoItem
		/// </summary >
		public String UserId { get; set; }

		/// <summary >
		/// List of labels associated with TodoItem
		/// </summary >
		public List<TodoItemLabel> Labels { get; set; }

		/// <summary >
		/// Date due . If null , no date was set by the user
		/// </summary >
		public DateTime? DateDue { get; set; }

		public TodoItem(string text, String userId)
		{
			Id = Guid.NewGuid();
			Text = text;
			DateCreated = DateTime.UtcNow;
			UserId = userId;
			Labels = new List<TodoItemLabel>();
			Completed = false;
		}

		public TodoItem(string text, String userId, DateTime? dateDue)
		{
			Id = Guid.NewGuid();
			Text = text;
			DateCreated = DateTime.UtcNow;
			UserId = userId;
			Labels = new List<TodoItemLabel>();
			Completed = false;
			DateDue = dateDue;
		}

		public TodoItem()
		{
			// entity framework needs this one
			// not for use :)
		}
	}

	/// <summary >
	/// Label describing the TodoItem .
	/// e.g. Critical , Personal , Work ...
	/// </summary >
	public class TodoItemLabel
	{
		public Guid Id { get; set; }
		public string Value { get; set; }

		/// <summary >
		/// All TodoItems that are associated with this label
		/// </summary >
		public List<TodoItem> LabelTodoItems { get; set; }

		public TodoItemLabel(string value)
		{
			Id = Guid.NewGuid();
			Value = value;
			LabelTodoItems = new List<TodoItem>();
		}

		public TodoItemLabel()
		{
			
		}
	}
}
