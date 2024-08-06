namespace E_Js.Base
{
    public abstract class BaseResponse:BaseMessage
    {
        public BaseResponse(Guid corelationId)
        {
            base._corelationId = corelationId;
        }
        public BaseResponse()
        {
            
        }
    }
}
