using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chirpy.Domain.Model;

namespace Chirpy.Domain.Repository
{
    public interface IChirpRepository
    {
        IList<Chirp> GetAllChirps();
        IList<Chirp> GetAllChirpMentions(int userId);
        IList<Domain.Model.Chirp> GetAllChirpReplies(int chirpId);

        IList<Chirp> GetChirps(int userId);
        IList<Chirp> GetAllChirpsByTag(string tag);
        IList<ChirpTag> GetAllChirpTags();

        void AddChirp(Chirp chirp);
        Chirp GetChirp(int id);
        void DeleteChirp(int id);
    }
}
