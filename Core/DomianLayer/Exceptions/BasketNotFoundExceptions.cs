namespace DomainLayer.Exceptions
{
    public class BasketNotFoundException(string id): NotFoundException($"The basket you are looking for does not exist with Id {id}")
    {
    }
}
