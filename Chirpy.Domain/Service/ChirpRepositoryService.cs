using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chirpy.Domain.Repository;
using Chirpy.Domain.Model;

namespace Chirpy.Domain.Service
{
    public class ChirpRepositoryService
    {
        IChirpRepository _repository;
        public ChirpRepositoryService(IChirpRepository repostiory)
        {
            _repository = repostiory;
        }

        public IList<Chirp> GetAllChirps()
        {
            return _repository.GetAllChirps();
        }

        public IList<Chirp> GetChirpFor(ChirpyUser user)
        {
            return _repository.GetChirps(user.Id);
        }

        public void AddChirp(Chirp newChirp)
        {
            string[] tokens = newChirp.Value.Split(new char[] { ' ' });
            for (int tokenCount = 0; tokenCount < tokens.Length; tokenCount++)
            {
                if (tokens[tokenCount].StartsWith("#"))
                {
                    newChirp.Tags.Add(new ChirpTag { Tag = tokens[tokenCount] });
                }
            }
            _repository.AddChirp(newChirp);
        }

        public Chirp GetChirp(int id)
        {
            return _repository.GetChirp(id);
        }

        public void DeleteChirp(int id)
        {
            _repository.DeleteChirp(id);
        }
    }
}
