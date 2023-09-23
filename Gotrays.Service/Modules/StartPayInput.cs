namespace Gotrays.Contract.Modules;

public class StartPayInput
{
    public int payType { get; set; }
    
    public Guid productId { get; set; }

    public StartPayInput(Guid productId)
    {
        this.productId = productId;
        payType = 0;
    }
}