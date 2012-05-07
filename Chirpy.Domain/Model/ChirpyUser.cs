using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chirpy.Domain.Model
{
    public class ChirpyUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
