using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository) 
        {
            _tagRepository = tagRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTag (string name)
        {
            await _tagRepository.CreateTag(name);
            return Ok();
        }
    }
}
