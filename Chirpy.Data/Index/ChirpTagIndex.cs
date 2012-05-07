using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Indexes;
using Raven.Abstractions.Indexing;

namespace Chirpy.Data.Index
{
    public class ChirpTagIndex : AbstractIndexCreationTask<Data.Model.Chirp, Data.Model.HashTagCount>
    {
        public ChirpTagIndex()
        {
            Map = chirps => from chirp in chirps
                           from tag in chirp.Tags
                           select new { Name = tag.ToString().ToLower(), Count = 1 };
            Reduce = results => from tagCount in results
                                group tagCount by tagCount.Tag
                                    into g
                                    select new { Tag = g.Key, Count = g.Sum(x => x.Count) };
            Sort(result => result.Count, SortOptions.Int);
        }
    }
}
