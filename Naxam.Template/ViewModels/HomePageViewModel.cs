using System;
using Prism.Navigation;

namespace Naxam.Template
{
	public class HomePageViewModel : CoreViewModel, INavigationPageOptions, IMasterDetailPageOptions
	{
		public bool ClearNavigationStackOnNavigation
		{
			get
			{
				return true;
			}
		}

		public bool IsPresentedAfterNavigation
		{
			get
			{
				return false;
			}
		}
	}
}
