using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Sale.Infra.Persistance.Nh.Order
{
    //public class OrderStateMapper:IUserType
    //{
    //    public bool Equals(object x, object y)
    //    {
    //        return x == y;
    //    }

    //    public int GetHashCode(object x)
    //    {
    //        return x.GetHashCode();
    //    }

    //    public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    //    {
    //        var res = rs[names[0]].ToString();
    //        var t = typeof(OrderState).Assembly.GetExportedTypes().Single(type => type.FullName == res);
    //        return Activator.CreateInstance(t, (Domain.OrderAgg.Order)owner);
    //    }

    //    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    //    {
    //        ((SqlParameter)cmd.Parameters[index]).Value = value.GetType().FullName;
    //    }

    //    public object NullSafeGet(IDataReader rs, string[] names, object owner)
    //    {
    //        var res = rs[names[0]].ToString();
    //        var t = typeof(OrderState).Assembly.GetExportedTypes().Single(type => type.FullName == res);
    //        return Activator.CreateInstance(t, (Domain.OrderAgg.Order) owner);
    //    }

    //    public void NullSafeSet(IDbCommand cmd, object value, int index)
    //    {
    //        ((SqlParameter)cmd.Parameters[index]).Value =  value.GetType().FullName;
    //    }

    //    public object DeepCopy(object value)
    //    {
    //        return value;
    //    }

    //    public object Replace(object original, object target, object owner)
    //    {
    //        return original;
    //    }

    //    public object Assemble(object cached, object owner)
    //    {
    //        return cached;
    //    }

    //    public object Disassemble(object value)
    //    {
    //        return value;
    //    }

    //    public SqlType[] SqlTypes { get { return new[] {new SqlType(DbType.String),}; } }
    //    public Type ReturnedType { get { return typeof(OrderState); } }
    //    public bool IsMutable { get { return true; } }
    //}
}