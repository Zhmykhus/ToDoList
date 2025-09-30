using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly DateTask { get; set; }
        public bool IsCompleted { get; set; }
    }
}
