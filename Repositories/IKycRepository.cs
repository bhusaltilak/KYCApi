using System.Collections.Generic;
using KycApi.Models;

namespace KycApi.Repositories
{
    public interface IKycRepository
    {
        List<KycModel> GetAll();
        KycModel GetById(int id);
        bool Create(KycModel kyc);
        bool Update(int id, KycModel kyc);
        bool Delete(int id);
    }
}

