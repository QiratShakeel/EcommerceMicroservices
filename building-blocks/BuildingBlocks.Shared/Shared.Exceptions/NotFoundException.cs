namespace BuildingBlocks.Shared.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity, object key) : base($"{entity} with {key} was not found")
        { }
    }
}