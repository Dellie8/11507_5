using System.Numerics;

public class Order<T> where T : INumber<T>
{
    public int Id { get; set; }
    public T Price { get; set; }
}

public abstract class OrderHandler<T> where T : INumber<T>
{
    protected OrderHandler<T>? Next;
    public void SetNext(OrderHandler<T> next) => Next = next;
    public abstract void Handle(Order<T> order);
}

public interface IIdStep { IPriceStep<T> SetId<T>(int id) where T : INumber<T>; }
public interface IPriceStep<T> where T : INumber<T> { IFinalStep<T> SetBasePrice(T price); }
public interface IFinalStep<T> where T : INumber<T> { Order<T> Build(); }

public class OrderBuilder : IIdStep
{
    private int _id;
    public static IIdStep Create() => new OrderBuilder();

    public IPriceStep<T> SetId<T>(int id) where T : INumber<T> => new InternalBuilder<T>(id);

    private class InternalBuilder<T>(int id) : IPriceStep<T>, IFinalStep<T> where T : INumber<T>
    {
        private T _price = T.Zero;
        public IFinalStep<T> SetBasePrice(T price) { _price = price; return this; }
        public Order<T> Build() => new Order<T> { Id = id, Price = _price };
    }
}

public class DiscountHandler<T>(T discount) : OrderHandler<T> where T : INumber<T>
{
    public override void Handle(Order<T> order)
    {
        order.Price -= discount;
        Next?.Handle(order);
    }
}

public class TaxHandler<T> : OrderHandler<T> where T : INumber<T>
{
    public override void Handle(Order<T> order)
    {
        order.Price *= T.CreateChecked(1.2);
        Next?.Handle(order);
    }
}

public class ValidationHandler<T> : OrderHandler<T> where T : INumber<T>
{
    public override void Handle(Order<T> order)
    {
        if (order.Price < T.Zero) throw new Exception("Цена не может быть отрицательной!!!");
        Next?.Handle(order);
    }
}

public void Process<T>(Order<T> order) where T : INumber<T>
{
    var discount = new DiscountHandler<T>(T.CreateChecked(100));
    var tax = new TaxHandler<T>();
    var validator = new ValidationHandler<T>();

    discount.SetNext(tax);
    tax.SetNext(validator);

    discount.Handle(order);
}