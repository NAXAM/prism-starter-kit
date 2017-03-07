using System;
using System.Collections.ObjectModel;

using Realms;

using Naxam.Template.Dal;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Prism.Navigation;

namespace Naxam.Template
{
	public class TodosPageViewModel : CoreViewModel
	{
		ICommand _AddTodoCommand;
		public ICommand AddTodoCommand { 
			get { return (_AddTodoCommand = _AddTodoCommand ?? new Command(HandleAddTodo)); }
		}
		async void HandleAddTodo() {
			await navigationService.NavigateAsync(Pages.AddTodo);
		}

		ObservableCollection<TodoModel> _Todos;
		public ObservableCollection<TodoModel> Todos
		{
			get { return _Todos; }

			set
			{
				if (ReferenceEquals(_Todos, value)) return;

				_Todos = value;
				OnPropertyChanged();
			}
		}

		readonly ITodoRepository todoRepository;
		readonly INavigationService navigationService;

		public TodosPageViewModel(INavigationService navigationService, ITodoRepository todoRepository)
		{
			this.navigationService = navigationService;
			this.todoRepository = todoRepository;
		}

		public override void OnNavigatedTo(Prism.Navigation.NavigationParameters parameters)
		{
			todoRepository.GetQueryable()
						  .SubscribeForNotifications(UpdateTodos);
		}

		void UpdateTodos(IRealmCollection<TodoItem> sender, ChangeSet changes, Exception error)
		{
			if (error != null)
			{
				return;
			}

			if (changes == null)
			{
				var todos = sender.Select(ToModel).ToList();

				Todos = new ObservableCollection<TodoModel>(todos);

				return;
			}

			for (int i = 0; i < changes.DeletedIndices?.Length; i++)
			{
				Todos.RemoveAt(changes.DeletedIndices[i]);
			}

			for (int i = 0; i < changes.InsertedIndices?.Length; i++)
			{
				var item = sender[changes.InsertedIndices[i]];
				Todos.Insert(changes.InsertedIndices[i], ToModel(item));
			}

			for (int i = 0; i < changes.ModifiedIndices?.Length; i++)
			{
				var item = sender[changes.ModifiedIndices[i]];
				var model = Todos[changes.ModifiedIndices[i]];

				model.Title = item.Title;
				model.Deadline = item.Deadline;
				model.Done = item.Done;
				model.HasDeadline = item.Deadline.HasValue;
			}

			for (int i = 0; i < changes.Moves?.Length; i++)
			{
				var move = changes.Moves[i];
				var temp = Todos[move.From];

				Todos[move.From] = Todos[move.To];
				Todos[move.To] = temp;
			}
		}

		TodoModel ToModel(TodoItem item)
		{
			if (item == null) return null;

			return new TodoModel
			{
				Id = item.Id,
				Title = item.Title,
				Done = item.Done,
				HasDeadline = item.Deadline.HasValue,
				Deadline = item.Deadline
			};
		}
	}
}
