using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Zadatak2.Models;

namespace Zadatak2.ProsliZadatak
{
	public class TodoDbContext : DbContext
	{
		public IDbSet<TodoItem> TodoItemSet { get; set; }
		public IDbSet<TodoItemLabel> TodoItemLabelSet { get; set; }
		//public IDbSet<UserManager<ApplicationUser>> userList { get; set; }

		public TodoDbContext(string cnnstr) : base(cnnstr)
		{
			
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TodoItem>().HasKey(s => s.Id);
			//modelBuilder.Entity<TodoItem>().Property(s => s.Labels).IsRequired();
			modelBuilder.Entity<TodoItem>().Property(s => s.Id).IsRequired();
			modelBuilder.Entity<TodoItem>().Property(s => s.Text).IsRequired();
			modelBuilder.Entity<TodoItem>().HasMany(item => item.Labels).WithMany(label => label.LabelTodoItems);


			modelBuilder.Entity<TodoItemLabel>().HasKey(s => s.Id);
			modelBuilder.Entity<TodoItemLabel>().Property(s => s.Value).IsRequired();
			modelBuilder.Entity<TodoItemLabel>().HasMany(label => label.LabelTodoItems).WithMany(item => item.Labels);
			//modelBuilder.Entity<TodoItemLabel>().Property(s => s.LabelTodoItems).IsRequired();

			//modelBuilder.Entity<TodoItem>().HasMany(ti => ti.Labels).WithMany(til => til.LabelTodoItems); //nije 100%

		}
	}
}
