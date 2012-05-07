using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chirpy.Domain.Model
{
    public class Chirp
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Chirp InReplyTo { get; set; }
        public IList<Chirp> Replies { get; set; }
        public IList<ChirpTag> Tags { get; set; }
        public ChirpyUser User;
        public Chirp()
        {
            Tags = new List<ChirpTag>();
            User = new ChirpyUser();
        }
    }
}
