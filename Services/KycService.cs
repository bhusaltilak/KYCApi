using System.Collections.Generic;
using KycApi.DTOs;
using KycApi.Models;
using KycApi.Repositories;

namespace KycApi.Services
{
    public class KycService : IKycService
    {
        private readonly IKycRepository _repository;

        public KycService(IKycRepository repository)
        {
            _repository = repository;
        }

        public List<KycModel> GetAll() => _repository.GetAll();
        public KycModel GetById(int id) => _repository.GetById(id);
        

        public bool Create(KycCreateDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Name))  
                return false;

            var kyc = new KycModel
            {
                Name = dto.Name ?? string.Empty,
                Address = dto.Address ?? string.Empty,
                Phone = dto.Phone ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Province = dto.Province ?? string.Empty,
                District = dto.District ?? string.Empty,
                VDC = dto.VDC ?? string.Empty
            };

            return _repository.Create(kyc);
        }
        public bool Update(int id, KycUpdateDto dto)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(dto.Name))
                return false;  

            var kyc = new KycModel
            {
                Id = id,
                Name = dto.Name ?? string.Empty,
                Address = dto.Address ?? string.Empty,
                Phone = dto.Phone ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Province = dto.Province ?? string.Empty,
                District = dto.District ?? string.Empty,
                VDC = dto.VDC ?? string.Empty
            };

            return _repository.Update(id, kyc);
        }

        public bool Delete(int id) => _repository.Delete(id);
    }
}
