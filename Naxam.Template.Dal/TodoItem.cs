using System;
using Realms;

namespace Naxam.Template.Dal
{
	public class TodoItem : RealmObject
	{
		public long Id { get; set; }

		public string Title { get; set; }

		public DateTimeOffset? Deadline { get; set; }

		public bool Done { get; set; }
	}
}
