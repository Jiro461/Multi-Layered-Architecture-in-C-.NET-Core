using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.DAL;
using SOA_BaiTap.RepositoryLayer.Interfaces;

namespace SOA_BaiTap.RepositoryLayer
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _context;
        public TagRepository(AppDbContext appDbContext) 
        {
            _context = appDbContext;
        }
        public async Task CreateTag(string tag)
        {
            if((await IsTagExist(tag)).Exist) 
            {
                throw new NotImplementedException();
            }
            await _context.Tags.AddAsync(new Tag
            {
                Name = tag,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTag(string tag)
        {
            var result = (await IsTagExist(tag));
            if (result.Exist && result.Tag != null)
            {
                _context.Tags.Remove(result.Tag);
                await _context.SaveChangesAsync();
                return;
            }
            throw new NotImplementedException("Không có Tag sau trong db");
        }

        public async Task UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
        public async Task<Tag?> GetTagByName (string tag)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
        }
        private async Task<Response> IsTagExist(string tag)
        {
            var result = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
            if (result == null)
            { 
                return
                new Response
                {
                    Exist = false,
                    Tag = null
                };
            }
            return new Response
            {
                Exist = true,
                Tag = result
            };
        }

        public async Task<List<Tag>> GetListTag (List<string> Names)
        {
            var Tags =  await _context.Tags.Where(t => Names.Any(Name => Name.ToLower() == t.Name)).ToListAsync();
            return Tags;
        }

        class Response
        {
            public bool Exist;
            public Tag? Tag;
        }
    }
}
