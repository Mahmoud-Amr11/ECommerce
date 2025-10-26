namespace Shared.DataTransferObject.BasketDtos
{
    public class BasketDto
    {
        public string Id{ get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; } = [];
    }
}
