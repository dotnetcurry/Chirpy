using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chirpy.Domain.Repository;
using Raven.Client;
using Chirpy.Data.Model;

namespace Chirpy.Data.Repository
{
    public class ChirpRepository : IChirpRepository
    {
        IDocumentStore _documentStore;
        string _databaseName = string.Empty;

        public ChirpRepository(IDocumentStore documentStore, string databaseName)
        {
            _documentStore = documentStore;
            _databaseName = databaseName;
        }

        public IList<Domain.Model.Chirp> GetAllChirps()
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                IEnumerable<Data.Model.Chirp> list = session.Query<Data.Model.Chirp>();
                var domainList = from chirp in list
                                 select chirp.ToDomainChirp();
                return domainList.ToList<Domain.Model.Chirp>();
            }
        }

        public IList<Domain.Model.Chirp> GetChirps(int userId)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                IQueryable<Data.Model.Chirp> list = session.Query<Data.Model.Chirp>();
                var domainList = from chirp in list
                                 where chirp.UserId == userId
                                 select chirp.ToDomainChirp();
                return domainList.ToList<Domain.Model.Chirp>();
            }
        }

        public IList<Domain.Model.Chirp> GetAllChirpMentions(int userId)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                IQueryable<Data.Model.Chirp> list = session.Query<Data.Model.Chirp>();
                var domainList = from chirp in list
                                 where chirp.UserId == userId
                                 select chirp.ToDomainChirp();
                return domainList.ToList<Domain.Model.Chirp>();
            }
        }

        public IList<Domain.Model.Chirp> GetAllChirpReplies(int chirpId)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                IQueryable<Data.Model.Chirp> list = session.Query<Data.Model.Chirp>();
                var domainList = from chirp in list
                                 where chirp.InReplyToId == chirpId
                                 select chirp.ToDomainChirp();
                return domainList.ToList<Domain.Model.Chirp>();
            }
        }

        public void AddChirp(Domain.Model.Chirp newChirp)
        {
            Data.Model.Chirp chirp = new Model.Chirp
            {
                Id = newChirp.Id,
                Value = newChirp.Value,
                InReplyToId = newChirp.InReplyTo != null ? newChirp.InReplyTo.Id : -1
            };
            chirp.Tags = (from tag in newChirp.Tags select tag.Tag).ToList<string>();
            SaveChirp(chirp);
        }

        private void SaveChirp(Data.Model.Chirp chirp)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                session.Store(chirp);
                session.SaveChanges();
            }
        }

        public void DeleteChirp(int id)
        {
            throw new NotImplementedException();
        }


        public IList<Domain.Model.Chirp> GetAllChirpsByTag(string tag)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                IQueryable<Data.Model.Chirp> chirpsByTagQuery = from chirp in session.Query<Data.Model.Chirp>()
                                                                where chirp.Tags.Any(currentTag => currentTag == tag)
                                                                select chirp;
                List<Data.Model.Chirp> chirpList = chirpsByTagQuery.ToList<Data.Model.Chirp>();
                List<Domain.Model.Chirp> domainChirps = (from chirp in chirpList
                                                         select chirp.ToDomainChirp()).ToList<Domain.Model.Chirp>();
                return domainChirps;
            }
        }

        public IList<Domain.Model.ChirpTag> GetAllChirpTags()
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                List<Domain.Model.ChirpTag> tags = new List<Domain.Model.ChirpTag>();
                var chirpIndex = session.Query<HashTagCount>("Chirps/HashTagCount");
                foreach (var item in chirpIndex)
                {
                    tags.Add(new Domain.Model.ChirpTag { TagCount = item.Count, Tag = item.Tag });
                }
                return tags;
            }
        }

        public Domain.Model.Chirp GetChirp(int id)
        {
            using (IDocumentSession session = _documentStore.OpenSession(_databaseName))
            {
                Data.Model.Chirp dataChirp = session.Load<Data.Model.Chirp>(id.ToString());
                return dataChirp.ToDomainChirp();
            }
        }
    }
}
