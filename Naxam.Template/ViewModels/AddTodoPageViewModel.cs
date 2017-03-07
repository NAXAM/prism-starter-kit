using System;
using System.Windows.Input;
using Naxam.Template.Dal;
using Prism.Navigation;
using Xamarin.Forms;

namespace Naxam.Template
{
	public class AddTodoPageViewModel : CoreViewModel
	{
		TodoModel _Todo;
		public TodoModel Todo
		{
			get { return _Todo; }

			set
			{
				if (Equals(_Todo, value)) return;

				_Todo = value;
				OnPropertyChanged();
			}
		}

		ICommand _AddTodoCommand;
		public ICommand AddTodoCommand
		{
			get { return (_AddTodoCommand = _AddTodoCommand ?? new Command(HandleAddTodo)); }
		}
		async void HandleAddTodo()
		{
			var item = new TodoItem
			{
				Title = Todo.Title,
				Done = Todo.Done,
				Deadline = Todo.HasDeadline ? Todo.Deadline : (DateTimeOffset?)null
			};

			var result = await todoRepository.Insert(item);

			if (result) await navigationService.GoBackAsync();
		}

		readonly INavigationService navigationService;
		readonly ITodoRepository todoRepository;

		public AddTodoPageViewModel(INavigationService navigationService, ITodoRepository todoRepository)
		{
			this.todoRepository = todoRepository;
			this.navigationService = navigationService;
		}

		public override void OnNavigatedTo(NavigationParameters parameters)
		{
			Todo = new TodoModel
			{
				Deadline = DateTime.Now.AddDays(1)
			};
		}
	}
}

