using QMS_APP.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace QMS_APP.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
