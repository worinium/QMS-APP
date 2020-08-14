using QMS_APP.Application.Common.Mappings;
using QMS_APP.Domain.Entities;

namespace QMS_APP.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
