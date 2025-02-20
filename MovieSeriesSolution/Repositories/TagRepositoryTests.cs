using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SOA_BaiTap.CoreLayer.Entities;
using SOA_BaiTap.DAL;
using SOA_BaiTap.RepositoryLayer;

namespace SOA_BaiTap.Tests.Repositories
{
    public class TagRepositoryTests
    {
        private readonly TagRepository _tagRepository;
        private readonly Mock<AppDbContext> _dbContextMock;
        private readonly Mock<DbSet<Tag>> _dbSetMock;

        public TagRepositoryTests()
        {
            _dbContextMock = new Mock<AppDbContext>();
            _dbSetMock = new Mock<DbSet<Tag>>();

            _dbContextMock.Setup(db => db.Tags).Returns(_dbSetMock.Object);

            _tagRepository = new TagRepository(_dbContextMock.Object);
        }

        [Fact]
        public async Task CreateTag_ShouldAddTag_WhenTagDoesNotExist()
        {
            // Arrange
            var tagName = "Action";

            // Giả lập kiểm tra tag không tồn tại
            _dbSetMock.Setup(db => db.AddAsync(It.IsAny<Tag>(), default)).Returns(await Task.CompletedTask);
            _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            await _tagRepository.CreateTag(tagName);

            // Assert
            _dbSetMock.Verify(db => db.AddAsync(It.Is<Tag>(t => t.Name == tagName), default), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task CreateTag_ShouldThrowException_WhenTagAlreadyExists()
        {
            // Arrange
            var tagName = "Action";
            var existingTag = new Tag { Name = tagName };

            _dbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>(), default))
                      .ReturnsAsync(existingTag);

            // Act & Assert
            await Assert.ThrowsAsync<NotImplementedException>(() => _tagRepository.CreateTag(tagName));

            _dbSetMock.Verify(db => db.AddAsync(It.IsAny<Tag>(), default), Times.Never);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Never);
        }

        // ✅ Kiểm thử xóa tag thành công
        [Fact]
        public async Task DeleteTag_ShouldRemoveTag_WhenTagExists()
        {
            // Arrange
            var tagName = "Action";
            var tag = new Tag { Name = tagName };

            _dbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>(), default))
                      .ReturnsAsync(tag);

            _dbSetMock.Setup(db => db.Remove(tag));
            _dbContextMock.Setup(db => db.SaveChangesAsync(default)).ReturnsAsync(1);

            // Act
            await _tagRepository.DeleteTag(tagName);

            // Assert
            _dbSetMock.Verify(db => db.Remove(tag), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        // ❌ Kiểm thử xóa tag bị lỗi (tag không tồn tại)
        [Fact]
        public async Task DeleteTag_ShouldThrowException_WhenTagDoesNotExist()
        {
            // Arrange
            var tagName = "Action";

            _dbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>(), default))
                      .ReturnsAsync((Tag)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotImplementedException>(() => _tagRepository.DeleteTag(tagName));

            Assert.Equal("Không có Tag sau trong db", exception.Message);

            _dbSetMock.Verify(db => db.Remove(It.IsAny<Tag>()), Times.Never);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Never);
        }

        // ✅ Kiểm thử cập nhật tag thành công
        [Fact]
        public async Task UpdateTag_ShouldUpdateTag_WhenTagExists()
        {
            // Arrange
            var tag = new Tag { Name = "Action" };

            // Act
            await _tagRepository.UpdateTag(tag);

            // Assert
            _dbSetMock.Verify(db => db.Update(tag), Times.Once);
            _dbContextMock.Verify(db => db.SaveChangesAsync(default), Times.Once);
        }

        // ✅ Kiểm thử lấy tag theo tên thành công
        [Fact]
        public async Task GetTagByName_ShouldReturnTag_WhenTagExists()
        {
            // Arrange
            var tagName = "Action";
            var tag = new Tag { Name = tagName };

            _dbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>(), default))
                      .ReturnsAsync(tag);

            // Act
            var result = await _tagRepository.GetTagByName(tagName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tagName, result.Name);
        }

        // ❌ Kiểm thử lấy tag theo tên bị lỗi (tag không tồn tại)
        [Fact]
        public async Task GetTagByName_ShouldReturnNull_WhenTagDoesNotExist()
        {
            // Arrange
            var tagName = "Action";

            _dbSetMock.Setup(db => db.FirstOrDefaultAsync(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>(), default))
                      .ReturnsAsync((Tag)null);

            // Act
            var result = await _tagRepository.GetTagByName(tagName);

            // Assert
            Assert.Null(result);
        }

        // ✅ Kiểm thử lấy danh sách tag theo tên thành công
        [Fact]
        public async Task GetListTag_ShouldReturnListOfTags_WhenTagsExist()
        {
            // Arrange
            var tagNames = new List<string> { "Action", "Drama" };
            var tags = new List<Tag>
            {
                new Tag { Name = "Action" },
                new Tag { Name = "Drama" }
            };

            _dbSetMock.Setup(db => db.Where(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>()))
                      .Returns(tags.AsQueryable());

            // Act
            var result = await _tagRepository.GetListTag(tagNames);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        // ❌ Kiểm thử lấy danh sách tag bị lỗi (không có tag nào)
        [Fact]
        public async Task GetListTag_ShouldReturnEmptyList_WhenNoTagsExist()
        {
            // Arrange
            var tagNames = new List<string> { "Action", "Drama" };

            _dbSetMock.Setup(db => db.Where(It.IsAny<System.Linq.Expressions.Expression<System.Func<Tag, bool>>>()))
                      .Returns(new List<Tag>().AsQueryable());

            // Act
            var result = await _tagRepository.GetListTag(tagNames);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
