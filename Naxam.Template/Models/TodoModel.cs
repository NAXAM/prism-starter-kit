using System;
using Prism.Mvvm;

namespace Naxam.Template
{
	public class TodoModel : BindableBase
	{
		public long Id { get; set; }

		string _Title;
		public string Title
		{
			get { return _Title; }

			set
			{
				if (string.Equals(_Title, value)) return;

				_Title = value;
				OnPropertyChanged();
			}
		}

		bool _Done;
		public bool Done
		{
			get { return _Done; }

			set
			{
				if (Equals(_Done, value)) return;

				_Done = value;
				OnPropertyChanged();
			}
		}

		bool _HasDeadline;
		public bool HasDeadline
		{
			get { return _HasDeadline; }

			set
			{
				if (Equals(_HasDeadline, value)) return;

				_HasDeadline = value;
				OnPropertyChanged();
			}
		}

		DateTimeOffset? _Deadline;
		public DateTimeOffset? Deadline
		{
			get { return _Deadline; }

			set
			{
				if (Equals(_Deadline, value)) return;

				_Deadline = value;
				OnPropertyChanged();
			}
		}
	}
}
