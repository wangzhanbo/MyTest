using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
