namespace Ecommerce.Catalog.Domain.ValueObjects
{
    public record ProductImage
    {
        public string Url { get; init; }
        public string AltText { get; init; }
        public string FileType { get; init; }
        private static readonly string[] AllowedFileTypes = { ".jpg", ".jpeg", ".png", ".gif" };

        private ProductImage() { } // EF
        public ProductImage(string url, string altText, string fileType)
        {
            if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Relative))
                throw new ArgumentException("Invalid URL format.", nameof(url));

            if (!AllowedFileTypes.Contains(fileType.ToLower()))
                throw new ArgumentException("Invalid file type.", nameof(fileType));

            Url = url;
            AltText = altText;
            FileType = fileType;
        }
    }
}
