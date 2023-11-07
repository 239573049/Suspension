namespace GotraysService.Contracts.Dtos.Admin;

public class TrendChart<T>
{
    public IEnumerable<TrendChartItem<decimal>> Items { get; set; }

    public T Sum { get; set; }
}

public class TrendChartItem<T>
{
    public string Date { get; set; }
    public T Value { get; set; }
}