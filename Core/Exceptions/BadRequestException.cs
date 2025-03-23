namespace Core.Exceptions
{
    public abstract class BadRequestException:Exception
    {
         public BadRequestException(string messsage):base(messsage)
        {
            
        }
    }
    public sealed class RefreshTokenBadRequest : BadRequestException
    {
        public RefreshTokenBadRequest()
        : base("Invalid client request. The tokenDto has some invalid values.")
        {
        }
    }
}
