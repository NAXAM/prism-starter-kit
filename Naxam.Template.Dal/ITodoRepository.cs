using System.Linq;
using System.Threading.Tasks;

namespace Naxam.Template.Dal
{
	public interface ITodoRepository
	{
		IQueryable<TodoItem> GetQueryable();

		Task<bool> Insert(TodoItem item);
		Task<bool> Update(TodoItem item);
		Task<bool> Delete(long id);
	}
}
