namespace Gotrays.Contract.Modules;

public class ChatProductDto
{
    public Guid id { get; set; }
    
    public string name { get; set; }
    
    public string description { get; set; }
    
    public double actualPrice { get; set; }
    
    public double originalPrice { get; set; }
    
    public int days { get; set; }
}