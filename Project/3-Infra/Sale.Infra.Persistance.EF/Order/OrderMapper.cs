namespace Sale.Infra.Persistance.Nh.Order
{
    //public class OrderMapper:ClassMap<Domain.OrderAgg.Order>
    //{
    //    public OrderMapper()
    //    {
    //        Id(order => order.Id).Access.CamelCaseField().GeneratedBy.Identity();
    //        Map(order => order.CustomerId).Access.CamelCaseField();
    //        Map(order => order.OrderState).Access.CamelCaseField().CustomType<OrderStateMapper>();
    //        Version(m => m.RowVersion).Access.CamelCaseField().Generated.Always().UnsavedValue(null);
    //        HasMany(order => order.OrderLines)
    //            .Access.CamelCaseField()
    //            .AsBag()
    //            .Cascade.AllDeleteOrphan()
    //            .KeyColumn("OrderId"); 
    //        OptimisticLock.Version();
    //    }
    //}
    //public class OrderLineMapper:ClassMap<Domain.OrderAgg.OrderLine>
    //{
    //    public OrderLineMapper()
    //    {
    //        Id(order => order.Id).Access.CamelCaseField().GeneratedBy.Identity();
    //        Map(order => order.ProductId).Access.CamelCaseField();
    //        Map(order => order.Quantity).Access.CamelCaseField();
    //    }
    //}
}