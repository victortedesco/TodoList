using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class Todo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}
