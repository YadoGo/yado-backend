using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using yado_backend.Models;
using yado_backend.Repositories;

namespace yado_backend.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(CacheProfileName = "CacheProfile1day")]
        public async Task<IActionResult> GetAllCompany()
        {
            var companies = await _companyRepository.GetAllCompany();
            return Ok(companies);
        }

        [Authorize(Roles = "3")]
        [HttpPost]
        public async Task<IActionResult> InsertCompany([FromBody] Company company)
        {
            if (await _companyRepository.InsertCompany(company))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "3")]
        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompanyById(int companyId, [FromBody] Company company)
        {
            if (await _companyRepository.UpdateCompanyById(companyId, company))
            {
                return Ok();
            }
            return NotFound();
        }

        [Authorize(Roles = "3")]
        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompanyById(int companyId)
        {
            if (await _companyRepository.DeleteCompanyById(companyId))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
