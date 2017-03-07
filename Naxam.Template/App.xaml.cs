using Microsoft.Practices.Unity;
using Naxam.Template.Dal;
using Prism.Unity;
using Xamarin.Forms;

namespace Naxam.Template
{
	public partial class App : PrismApplication
	{
		protected override void OnInitialized()
		{
			NavigationService.NavigateAsync($"{Pages.NavBar}/{Pages.Todos}");
		}

		protected override void RegisterTypes()
		{
			Container.RegisterInstance(Realms.Realm.GetInstance());

			Container.RegisterType<ITodoRepository, TodoRepository>();

			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<HomePage>();
			Container.RegisterTypeForNavigation<ProfilePage>();
			Container.RegisterTypeForNavigation<TodosPage>();
			Container.RegisterTypeForNavigation<AddTodoPage>();
		}
	}

	public static class Pages
	{
		public static string NavBar = nameof(NavigationPage);
		public static string Home = nameof(HomePage);
		public static string Todos = nameof(TodosPage);
		public static string AddTodo = nameof(AddTodoPage);
		public static string Profile = nameof(ProfilePage);
	}
}
