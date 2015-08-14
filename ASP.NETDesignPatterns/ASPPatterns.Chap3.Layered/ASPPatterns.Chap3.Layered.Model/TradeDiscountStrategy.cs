
namespace ASPPatterns.Chap3.Layered.Model
{
    public class TradeDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyExtraDiscountsTo(decimal originalSalePrice)
        {
            var price = originalSalePrice;
            price *= 0.95M; //5% Discount.
            return price;
        }
    }
}
