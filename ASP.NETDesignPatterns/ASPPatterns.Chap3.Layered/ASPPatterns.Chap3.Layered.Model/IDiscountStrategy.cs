
namespace ASPPatterns.Chap3.Layered.Model
{
    /// <summary>
    /// The Strategy pattern enables an algorithm to be encapsulated within a class and
    /// switched at runtime to alter an object’s behavior
    /// </summary>
    public interface IDiscountStrategy
    {
        decimal ApplyExtraDiscountsTo(decimal originalSalePrice);
    }
}
