using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface ITaskContainer
    {
        void AddSubTask(IAssignable task);

        void RemoveTask(IAssignable task);

        void RemoveTaskAt(int index);

        int SubTasksCount { get; }

        IAssignable[] SubTasks { get; }

        IAssignable this[int index] { get;set; }
    }
}
