using SOA_BaiTap.CoreLayer.Entities;

namespace SOA_BaiTap.RepositoryLayer.Interfaces
{
    public interface ITagRepository
    {
        public Task CreateTag (string tag);
        public Task DeleteTag (string tag);
        public Task UpdateTag (Tag tag);
        public Task<Tag?> GetTagByName(string tag);
        public Task<List<Tag>> GetListTag(List<string> Name);
    }
}
