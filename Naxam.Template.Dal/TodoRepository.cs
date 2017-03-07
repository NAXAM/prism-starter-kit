using System;
using System.Linq;
using System.Threading.Tasks;
using Realms;

namespace Naxam.Template.Dal
{
	public class TodoRepository : ITodoRepository
	{
		readonly Realm realm;

		public TodoRepository(Realm realm)
		{
			this.realm = realm;
		}

		public async Task<bool> Delete(long id)
		{
			var result = false;
			var item = realm.Find<TodoItem>(id);

			if (item == null) return result;

			result = await MakeChange(r => r.Remove(item));

			return result;
		}

		public IQueryable<TodoItem> GetQueryable()
		{
			return realm.All<TodoItem>();
		}

		public async Task<bool> Insert(TodoItem item)
		{
			var result = await MakeChange(r => r.Add(item));

			return result;
		}

		public async Task<bool> Update(TodoItem item)
		{
			var result = await MakeChange(r => r.Add(item, true));

			return result;
		}

		public async Task<bool> MakeChange(Action<Realm> action) { 
			var result = false;

			await realm.WriteAsync(action).ContinueWith(t =>
			{
				result = !t.IsFaulted && !t.IsCanceled && t.IsCompleted;
			});

			return result;
		}
	}
}
