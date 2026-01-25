using System.Collections.Generic; // Required for ICollection
namespace Ecommerce.Catalog.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }       
        public int? ParentCategoryId { get; private set; }

        private readonly List<Category> _children = new();
        public IReadOnlyCollection<Category> Children => _children.AsReadOnly();

        private Category() { } // EF

        public Category(string name, string description, int? parentId = null)
        {
            SetName(name);
            Description = description;
            ParentCategoryId = parentId;
        }
        public void UpdateCategory(string name, string description, int? parentId = null)
        {
            SetName(name);
            Description = description;
            ParentCategoryId = parentId;
        }
        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Category name is required");

            Name = newName;
        }

        public void AddChildCategory(Category child)
        {
            if (ReferenceEquals(child, this))
                throw new InvalidOperationException("A category cannot be its own child");

            if (_children.Contains(child))
                throw new InvalidOperationException("Child category already exists");


            _children.Add(child);
        }
    }

}