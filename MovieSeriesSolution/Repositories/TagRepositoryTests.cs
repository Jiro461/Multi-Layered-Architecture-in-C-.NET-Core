using Microsoft.EntityFrameworkCore;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.DAL;
using SOA_BaiTap.RepositoryLayer;

public class TagRepositoryTests
{
    private readonly TagRepository _tagRepository;
    private readonly AppDbContext _dbContext;

    public TagRepositoryTests()
    {
        // 🔹 Cấu hình DbContextOptions để kết nối SQL Server
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=DESKTOP-DVFH4C1\\SQLEXPRESS;Database=MoviesAppDB;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options;

        // 🔹 Khởi tạo AppDbContext với provider SQL Server
        _dbContext = new AppDbContext(options);
        _tagRepository = new TagRepository(_dbContext);
    }

    [Fact]
    public async Task CreateTag_ShouldAddTag_WhenTagDoesNotExist()
    {
        // Arrange
        var tagName = "NewTag2";

        // Act
        await _tagRepository.CreateTag(tagName);
        var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name == tagName);

        // Assert
        Assert.NotNull(tag);
        Assert.Equal(tagName, tag.Name);
    }
    [Fact]
    public async Task DeleteTag_ShouldRemoveTag_WhenTagExists()
    {
        // Arrange
        var tagName = "Action";
        var tag = new Tag { Name = tagName };
        await _dbContext.Tags.AddAsync(tag);
        await _dbContext.SaveChangesAsync();

        // Act
        await _tagRepository.DeleteTag(tagName);
        var result = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name == tagName);

        // Assert
        Assert.Null(result);
    }


}