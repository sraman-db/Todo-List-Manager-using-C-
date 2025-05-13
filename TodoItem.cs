using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager
{
    // This file contains the data model for todo items
    public class TodoItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public bool Completed { get; set; }
        public bool Pinned { get; set; }
        public DateTime CreatedAt { get; set; }

        public TodoItem()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            Priority = "Medium";
            Completed = false;
            Pinned = false;
        }

        public TodoItem(string title, string description = "", string priority = "Medium")
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            Priority = priority;
            Completed = false;
            Pinned = false;
            CreatedAt = DateTime.Now;
        }
    }
}