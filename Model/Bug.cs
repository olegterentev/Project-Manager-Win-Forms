using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class Bug : IAssignable
    {
        private User performer;

        public int PerformersCount
        {
            get
            {
                return performer == null ? 0 : 1;
            }
        }

        public int MaxPerformersCount { get { return 1; } }

        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreationDate { get; set; }
        public ITaskContainer Parent { get; set; }

        public void AssignPerformer(User performer)
        {
            if (this.performer != null)
                throw new Exception("Исполнитель уже назначен");
            this.performer = performer;
        }

        public void DischargePerformer(User performer)
        {
            if (this.performer == performer)
                this.performer = null;
        }

        public User[] Performers()
        {
            return new User[] { performer };
        }

        public override string ToString()
        {
            return $"Тип объекта: Ошибка.\r\n " +
                   $"Наименование: {Name}\r\n " +
                   $"Дата создания: {CreationDate}\r\n " +
                   $"Статус: {Status.GetDescription()}\r\n " +
                   $"Исполнитель: {performer}";
        }
    }
}
