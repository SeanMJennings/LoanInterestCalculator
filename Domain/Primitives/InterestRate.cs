namespace Domain.Primitives;

public readonly record struct InterestRate
{
    private decimal Value { get; }
    
    public InterestRate(string value) : this(decimal.Parse(value)){}
    public InterestRate(decimal value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Math.Round(Value, 2)}%";
    }
    
    public static implicit operator decimal(InterestRate interestRate) => interestRate.Value;
    
    public static implicit operator InterestRate(decimal value) => new(value);
}