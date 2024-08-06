namespace E_Js.Base
{
    public abstract class BaseMessage
    {
        protected Guid _corelationId = Guid.NewGuid();
        public Guid CorreltaionId() => _corelationId;
    }
}
