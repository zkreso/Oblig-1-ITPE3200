using System;
using Oblig_1_ITPE3200.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Oblig_1_ITPE3200.DAL
{
    public interface IKalkulatorRepository
    {
        Task<List<Sykdom>> HentSykdom();
        Task<bool> Lagre(Sykdom nySykdom);
        Task<Sykdom> HentEn(int id);
        Task<bool> Endre(Sykdom innSykdom);
        Task<bool> Slett(int id);
    }
}

