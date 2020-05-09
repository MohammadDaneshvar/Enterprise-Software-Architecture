using Framework.Validation;

namespace Sale.Domain.OrderAgg
{
    public class CouponCode
    {
        public string Code { get; private set; }
        public CouponTarget Target { get; private set; }

        public CouponCode(string code, CouponTarget target)
        {
            code.MustBeNotNull("couponcode must have value");
            Code = code;
            Target = target;
        }
    }
}