using WebStore.Domain.Entity;

namespace WebStore.Interfaces.DataProviders
{
    public interface ICartStore
    {
        Cart Cart { get; set; } 
    }
}
