using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.Entities
{
    public class UpdateTaskRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public bool Done { get; set; }
    }
}

