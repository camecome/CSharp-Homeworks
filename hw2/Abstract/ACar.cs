namespace hw2.Abstract
{
    public abstract class ACar : ICar
    {
        protected int seats;
        protected string transmission = "";
        protected string engine = "";

        public virtual string GetDescription()
        {
            return $"{GetType().Name}: {engine}, {transmission}, {seats} seats";
        }
    }
}
