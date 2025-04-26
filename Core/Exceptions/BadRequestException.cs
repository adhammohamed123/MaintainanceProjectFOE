using Core.Entities;

namespace Core.Exceptions
{
    public abstract class BadRequestException:Exception
    {
         public BadRequestException(string messsage):base(messsage)
        {
            
        }
    }
    public class RegionAlreadyRegistered : BadRequestException
    {
        public RegionAlreadyRegistered(string name) : base($"هذا القطاع:{name} موجود بالفعل")
        {
        }
    }
    public class GateAlreadyRegistered : BadRequestException
    {
        public GateAlreadyRegistered(string name) : base($"هذه البوابه :{name} موجودة بالفعل")
        {
        }
    }
    public class DepartmentAlreadyRegistered : BadRequestException
    {
        public DepartmentAlreadyRegistered(string name) : base($"هذه الادارة:{name} موجودة بالفعل")
        {
        }
    }
    public class OfficeAlreadyRegistered : BadRequestException
    {
        public OfficeAlreadyRegistered(string name) : base($"هذا المكتب:{name} موجود بالفعل")
        {
        }
    }
    public sealed class RefreshTokenBadRequest : BadRequestException
    {
        public RefreshTokenBadRequest()
        : base("برجاء الخروج والدخول مرة اخري ")
        //: base("Invalid client request. The tokenDto has some invalid values.")
        {
        }
    }
    public sealed class CannotDeleteMaintainanceRecordThatIsNotDelivered: BadRequestException
    {
        public CannotDeleteMaintainanceRecordThatIsNotDelivered(int maintainId)
            : base($"لا يمكن حذف عملية صيانة {maintainId} لانه لم تم تسليمها")
        //: base($"Cannot Delete Maintainance Record {maintainId} That Is Not Delivered!")
        {
        }
    }
    public sealed class CannotDeleteParentObjectThatHasChildrenException: BadRequestException
    {
        public CannotDeleteParentObjectThatHasChildrenException(string name)
            : base($"لا يمكن حذف {name} لانه يحتوي على عناصر فرعية")
        //: base($"Cannot Delete Parent Object {name} That Has Children!")
        {
        }
    }
    public sealed class CannotDeliverDeviceThatIsNotAssignedToAnyone : BadRequestException
    {
        public CannotDeliverDeviceThatIsNotAssignedToAnyone()
            : base($"لا يمكن تسليم الجهاز لانه لا يوجد قائم بالصيانة")
        //: base($"Cannot Deliver Device {deviceId} That Is Not Assigned To Anyone!")
        {
        }
    }
    public sealed class CannotUpdateMaintainanceRecordThatIsAlreadyDelivered : BadRequestException
    {
        public CannotUpdateMaintainanceRecordThatIsAlreadyDelivered()
            : base($"لا يمكن تعديل عملية صيانة تم تسليمها")
        //: base($"Cannot Update Maintainance Record {maintainId} That Is Already Delivered!")
        {
        }
    }
    public sealed class CannotDeliverDeviceThatIsDelivered : BadRequestException
    {
        public CannotDeliverDeviceThatIsDelivered()
            : base($"لا يمكن تسليم الجهاز لانه تم تسليمه من قبل")
        //: base($"Cannot Deliver Device {deviceId} That Is Delivered!")
        {
        }
    }
    public sealed class CannotDeliverDeviceBecauseStateIsNotWorkingOn: BadRequestException
    {
        public CannotDeliverDeviceBecauseStateIsNotWorkingOn()
            : base($"لا يمكن تسليم الجهاز لانه ليس في حالة صيانة")
        //: base($"Cannot Deliver Device {deviceId} Because State Is Not Working On!")
        {
        }
    }
    public sealed class DeviceAlreadyExistsException : BadRequestException
    {
        public DeviceAlreadyExistsException(string name)
            : base($"الجهاز {name} موجود بالفعل")
        //: base($"Device {name} Already Exists!")
        {
        }
    }
    public sealed class PasswordNotCorrectBadRequestException: BadRequestException
    {
        public PasswordNotCorrectBadRequestException(string name)
            : base($"كلمة المرور غير صحيحة للمستخدم {name}")
        //: base($"Password Not Correct For User {name}!")
        {
        }
    }
    public sealed class CannotUpdateMaintainanceRecordWhileItIsDone: BadRequestException
    {
        public CannotUpdateMaintainanceRecordWhileItIsDone(int maintainId)
            : base($"لا يمكن تعديل عملية صيانة تم اغلاقها")
    //: base($"Cannot Update Maintainance Record {maintainId} While It Is Done!")
    {
    }
}
public sealed class WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice : BadRequestException
    {
        public WeCannotOpenANewMaintainanceOperationWhileAnotehrOneIsNotEndedForTheSameDevice(int maintainId,int deviceId)
            :base("لا يمكن فتح عملية صيانة جديدة حاليا طالما لم يتم اغلاق العملية الحالية")
       // : base($"We Cannot Open A New Maintainance Operation {maintainId} While Anotehr One Is Not Ended For The Same Device {deviceId}")
        {
        }
    } 
    public sealed class CannotDeliverDeviceWithNotSolvedFailures: BadRequestException
    {
        public CannotDeliverDeviceWithNotSolvedFailures(int deviceId)
        : base($"لا يمكن تسليم الجهاز لوجود أعطال لم يتم حلها")
        //: base($"Cannot Deliver Device {deviceId} While Failures Not Solved!\nSolve these failures first!\n")
        {
        }
    }
    public sealed class CannotMakeMaintenanceStateDoneOrCancelledWhileDeviceHasNotSolvedFailures: BadRequestException
    {
        public CannotMakeMaintenanceStateDoneOrCancelledWhileDeviceHasNotSolvedFailures(int deviceId)
        : base($"لا يمكن تغيير حالة عملية الصيانة لوجود أعطال لم يتم حلها")
        //: base($"Cannot Make Maintenance State Done Or Canceled While Device {deviceId} Has Failures Are Not Solved!\n Solve these failures first! \n ")
        {
        }
    }
}
