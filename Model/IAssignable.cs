using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public interface IAssignable
    {
        string Name { get; set; }
        DateTime CreationDate { get; set; }
        TaskStatus Status { get; set; }
        ITaskContainer Parent { get; set; }
        int PerformersCount { get; }
        int MaxPerformersCount { get; }
        void AssignPerformer(User performer);
        void DischargePerformer(User performer);
        User[] Performers();
    }
}
