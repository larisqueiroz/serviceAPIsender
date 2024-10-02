using ServiceAPISender.Models;

namespace ServiceAPISender.Models;

public class Message
{
    public int CodigoPedido { get; set; }
    public int CodigoCliente { get; set; }
    public List<ItemDto> Itens { get; set; } = new List<ItemDto>();
}