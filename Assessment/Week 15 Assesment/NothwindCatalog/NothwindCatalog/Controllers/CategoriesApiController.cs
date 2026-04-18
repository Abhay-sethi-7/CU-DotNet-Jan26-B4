using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc;
    using NorthwindCatalog.Services.Repositories;
    using NorthwindCatalog.Services.DTOs;
    using AutoMapper;
namespace NothwindCatalog.Web.Controllers
{
  

    [ApiController]
    [Route("api/categories")]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoriesApiController(ICategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _repo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return Ok(result);
        }
    }
}
