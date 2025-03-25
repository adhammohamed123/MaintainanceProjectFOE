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
    public sealed class WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice : BadRequestException
    {
        public WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice(int maintainId,int deviceId)
        : base($"We Cannot Open A New Maintainance Operation {maintainId} While Anotehr One Is Not Ended For The Same Device {deviceId}")
        {
        }
    }
}
