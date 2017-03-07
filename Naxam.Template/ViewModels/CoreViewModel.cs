using System;
using Prism.Mvvm;
using Prism.Navigation;

namespace Naxam.Template
{
	public abstract class CoreViewModel : BindableBase, INavigationAware
	{
		public virtual void OnNavigatedFrom(NavigationParameters parameters)
		{
			
		}

		public virtual void OnNavigatedTo(NavigationParameters parameters)
		{
			
		}
	}
}
