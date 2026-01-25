using Ecommerce.Catalog.Domain.Entities;
using System;
using Xunit;

namespace Catalog.Domain.Tests.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void CreateCategory_WithValidName_ShouldSucceed()
        {
            var category = new Category("Electronics", "All electronic items");
            Assert.Equal("Electronics", category.Name);
            Assert.Equal("All electronic items", category.Description);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateCategory_WithInvalidName_ShouldThrow(string invalidName)
        {
            Assert.Throws<ArgumentException>(() => new Category(invalidName, "desc"));
        }

        [Fact]
        public void UpdateCategory_ShouldChangeValues()
        {
            var category = new Category("Old", "Old Desc");
            category.UpdateCategory("New", "New Desc", 2);

            Assert.Equal("New", category.Name);
            Assert.Equal("New Desc", category.Description);
            Assert.Equal(2, category.ParentCategoryId);
        }

        [Fact]
        public void AddChildCategory_ShouldAddChild()
        {
            var parent = new Category("Parent", "desc");
            var child = new Category("Child", "desc");

            parent.AddChildCategory(child);

            Assert.Single(parent.Children);
            Assert.Contains(child, parent.Children);
        }

        [Fact]
        public void AddChildCategory_SameAsParent_ShouldThrow()
        {
            var parent = new Category("Parent", "desc");
            Assert.Throws<InvalidOperationException>(() => parent.AddChildCategory(parent));
        }

        [Fact]
        public void AddChildCategory_DuplicateChild_ShouldThrow()
        {
            var parent = new Category("Parent", "desc");
            var child = new Category("Child", "desc");
            parent.AddChildCategory(child);

            Assert.Throws<InvalidOperationException>(() => parent.AddChildCategory(child));
        }
    }
}
