using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chirpy.Data.Model
{
    public class HashTagCount
    {
        public string Tag { get; set; }
        public int Count { get; set; }

        internal Domain.Model.ChirpTag ToDomainChirpTag()
        {
            return new Domain.Model.ChirpTag { Tag = this.Tag, TagCount = this.Count };
        }
    }
}
