using System;
using System.ComponentModel.DataAnnotations;

namespace MongoDB
{
    public class Pessoa
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}