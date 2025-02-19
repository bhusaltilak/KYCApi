using System.Collections.Generic;
using KycApi.DTOs;
using KycApi.Models;

namespace KycApi.Services
{
    public interface IKycService
    {
        List<KycModel> GetAll();
        KycModel GetById(int id);
        bool Create(KycCreateDto kycDto);
        bool Update(int id, KycUpdateDto kycDto);
        bool Delete(int id);
    }
}
