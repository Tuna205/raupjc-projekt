using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak2.ProsliZadatak;

namespace Zadatak2.Services
{
    public class LabelManager
    {
	    private TodoItem _item;
	    public HashSet<String> _labeleString{ get; set; }
		public HashSet<TodoItemLabel> _labels { get; set; }
	    private ITodoRepository _repo;

	    public LabelManager(TodoItem item, string labele, ITodoRepository repo)
	    {
		    _item = item;
			_labeleString = new HashSet<string>();
			_labels = new HashSet<TodoItemLabel>();
		    try
		    {
			    String[] labeleArr = labele.Split(',');
				foreach (string labela in labeleArr)
				{ 
				    _labeleString.Add(labela.ToLower().Trim());
			    }
			}
			catch { }
		    
	    }

	    public TodoItem GetItemWithLabels()
	    {
		    foreach (var s in _labeleString)
		    {
				TodoItemLabel itemLabel = new TodoItemLabel(s);
			    itemLabel.LabelTodoItems.Add(_item);
				_item.Labels.Add(itemLabel);
				
			    _labels.Add(itemLabel);
		    }
		    return _item;
	    }
    }
}
