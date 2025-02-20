using Xunit;
using System.ComponentModel.DataAnnotations;
using SOA_BaiTap.CoreLayer.Entities;
using System.Collections.Generic;

namespace MovieSeries.Tests.Models
{
    public class MovieTests
    {
        [Fact]
        public void Movie_ShouldRequireTitle()
        {
            // Arrange
            var movie = new Movie { Genre = "Sci-Fi" }; // Thiếu Title

            // Act
            var validationResults = ValidateModel(movie);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Title"));
        }

        [Fact]
        public void Movie_ShouldRequireGenre()
        {
            // Arrange
            var movie = new Movie { Title = "Inception" }; // Thiếu Genre

            // Act
            var validationResults = ValidateModel(movie);

            // Assert
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Genre"));
        }

        private List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
