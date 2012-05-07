using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chirpy.Domain.Model
{
    public class ChirpTag
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public int TagCount { get; set; }
    }
}
