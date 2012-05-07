using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chirpy.Data.Model
{
    public class Chirp
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int UserId {get;set;}
        public int InReplyToId { get; set; }
        public IList<int> ChirpReplies { get; set; }
        public IList<string> Tags { get; set; }

        internal Domain.Model.Chirp ToDomainChirp()
        {
            Domain.Model.Chirp domainChirp= new Domain.Model.Chirp
            {
                Id = this.Id,
                Value = this.Value
            };
            if (this.ChirpReplies != null)
            {
                //foreach (Data.Model.Chirp chirp in this.ChirpReplies)
                //{
                //    domainChirp.Replies.Add(chirp.ToDomainChirp());
                //}
            }
            foreach (string tag in Tags)
            {
                domainChirp.Tags.Add(new Domain.Model.ChirpTag { Tag = tag });
            }
            return domainChirp;
        }
    }
}
