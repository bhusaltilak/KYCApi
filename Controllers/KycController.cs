using Microsoft.AspNetCore.Mvc;
using KycApi.Services;
using KycApi.DTOs;

namespace KycApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KycController : ControllerBase
    {
        private readonly IKycService _service;


        public KycController(IKycService service) 
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var records = _service.GetAll();
            return Ok(records);
        }

        [HttpGet("{id}")] public IActionResult GetById(int id) => _service.GetById(id) is var kyc ? Ok(kyc) : NotFound();
       
        [HttpPost]
        public IActionResult Create([FromBody] KycCreateDto dto) 
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))  
            {
                return BadRequest(new { Message = "Invalid input. Name is required." });
            }

            bool success = _service.Create(dto);
            if (success)
                return Ok(new { Message = "KYC record created successfully" });

            return BadRequest(new { Message = "Failed to create KYC record" });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, KycUpdateDto dto) => _service.Update(id, dto) ? Ok("Updated Successfully") : NotFound();
        [HttpDelete("{id}")] public IActionResult Delete(int id) => _service.Delete(id) ? Ok("Deleted Successfully") : NotFound();
    }
}
