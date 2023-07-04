using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Epic : IAssignable, ITaskContainer
    {
        private List<User> performers;
        private List<IAssignable> subtasks;
        private int maxPerformersCount;

        public Epic(int maxPerformersCount = 10)
        {
            performers = new List<User>();
            subtasks = new List<IAssignable>();
            this.maxPerformersCount = maxPerformersCount;
        }

        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public int PerformersCount
        {
            get { return performers.Count; }
        }

        public int MaxPerformersCount
        {
            get { return maxPerformersCount; }
        }

        public void AssignPerformer(User performer)
        {
            if (performers.Count == maxPerformersCount)
                throw new Exception("Превышено допустимое количество исполнителей!");
            if (performers.Contains(performer))
                throw new Exception("Этот исполнитель уже назначен на эту задачу!");
            performers.Add(performer);
        }

        public void DischargePerformer(User performer)
        {
            if (performers.Contains(performer))
                performers.Remove(performer);
        }

        public User[] Performers()
        {
            return performers.ToArray();
        }

        public void AddSubTask(IAssignable task)
        {
            if (task.GetType() == typeof(Epic) || task.GetType() == typeof(Bug))
                throw new Exception("Невозможно добавить данный тип задачи");
            task.Parent = this;
            subtasks.Add(task);
        }

        public void RemoveTask(IAssignable task)
        {
            subtasks.Remove(task);
        }

        public void RemoveTaskAt(int index)
        {
            subtasks.RemoveAt(index);
        }

        public int SubTasksCount
        {
            get { return subtasks.Count; }
        }

        public IAssignable[] SubTasks
        {
            get { return subtasks.ToArray(); }
        }

        public ITaskContainer Parent { get; set; }

        public IAssignable this[int index]
        {
            get
            {
                return subtasks[index];
            }
            set
            {
                subtasks[index] = value;
            }
        }

        public override string ToString()
        {
            return $"Тип объекта: Тема.\r\n " +
                   $"Наименование: {Name}\r\n " +
                   $"Дата создания: {CreationDate}\r\n " +
                   $"Статус: {Status.GetDescription()}\r\n " +
                   $"Максимальное количество исполнителей: {maxPerformersCount}\r\n " +
                   $"Текущее количество задач: {subtasks.Count}\r\n " +
                   $"Исполнители: {string.Join("\r\n\t", performers)}";
        }
    }
}
