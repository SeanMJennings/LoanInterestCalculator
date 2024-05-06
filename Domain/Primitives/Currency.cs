using System.Globalization;
using Utilities;

namespace Domain.Primitives;

public readonly record struct Currency
{
    public RegionInfo RegionInfo { get; } = null!;

    public Currency(string countryCode)
    {
        Validation.BasedOn(errors =>
        {
            if (!Regions.IsSupportedCountryCode(countryCode))
            {
                errors.Add("The code provided is unknown.");
            }
        });
        RegionInfo = new RegionInfo(countryCode);
    }

    public override string ToString()
    {
        return RegionInfo.ISOCurrencySymbol;
    }

    public static implicit operator string(Currency currency) => currency.ToString();
    
    public static implicit operator Currency(string code) => new(code);
}