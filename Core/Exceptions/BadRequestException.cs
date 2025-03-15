namespace Core.Exceptions
{
    public abstract class BadRequestException:Exception
    {
         public BadRequestException(string messsage):base(messsage)
        {
            
        }
    }
}
