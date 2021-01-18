using Demo.Persistence;
using Demo.Services;
using Demo.Shared.Models.Domain;
using Demo.Shared.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private DemoDbContext _dbContext { get; set; }
        public ILogger<CompanyController> _logger { get; }

        public CompanyController(DemoDbContext dbContext, ILogger<CompanyController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        //[Authorize(Policy = Policy.)]
        public IActionResult All()
        {
            return Ok(_dbContext.Companies.Select(c=> new CompanyResponse() { 
                Id = c.Id,
                Name = c.Name
            }).ToList());
        }
    }
}
