namespace Services.Hateoas.Models
{
    public class ObjectResource<T> : Resource
    {
        public ObjectResource(T data) : base(data) { }

        public new T Data => (T) base.Data;
    }
}