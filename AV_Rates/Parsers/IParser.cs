namespace AV_Rates.Parsers
{
    interface IParser<F, T>
    {
        T ParseCurrency(F input);
    }
}
