using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string? message) : base(message)
        {
        }
    }

    public class DeviceNotFoundException : NotFoundException
    {
        public DeviceNotFoundException(int id) :
            base($"الجهاز غير موجود!")
            //base($"Device with Id {id} not found in our App")
        {
        }
    }

    public class RegionNotFoundException : NotFoundException
    {
        public RegionNotFoundException(int id) : 
            base($"لا يوجد قطاع بهذا الاسم")
            //base($"Sorry no Regions Registered with this Id {id}")
        {
        }
    }

    public class GateNotFoundException : NotFoundException 
    {
        public GateNotFoundException(int id):
            base($"لا توجد بوابة بهذا الاسم")
            //base($"No Gate Available With This Id {id}")
        {
            
        }
    }

    public class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException(int id):
            base($"لا توجد ادارة بهذا الاسم")
            //base($"no Department exists with this id {id} for that specific Gate")
        {
            
        }
    }
    public class OfficeNotFoundException : NotFoundException
    {
        public OfficeNotFoundException(int id) : 
            base($"لا يوجد مكتب بهذا الاسم")
            //base($"no office exists with this id {id}")
        {
        }
    }
	public class FailureNotFoundException : NotFoundException
	{
		public FailureNotFoundException(int id) : 
            base($"لا يوجد عطل بهذا الاسم")
            //base($"no Failure exists with this id {id}")
		{
		}
	}public class FailureMaintainNotFoundException : NotFoundException
	{
		public FailureMaintainNotFoundException(int maintainId,int failureId) : 
            base($"لا يوجد عطل لهذه العملية بهذا الاسم")
            //base($"no Failure Maintain exists with these MaintainId {maintainId} and FailureId {failureId}")
		{
		}
	}
	public class UserNotFoundException : NotFoundException
	{
		public UserNotFoundException(string id) : 
            base($"لا يوجد مستخدم بهذا الاسم")
            //base($"no User exists with this id {id}")
		{
		}
	}
    public class DeviceFailureHistoryNotFoundException:NotFoundException
    {
		public DeviceFailureHistoryNotFoundException(int id) : 
            base($"لا يوجد عملية صيانة بهذا الاسم")
            //base($"no DeviceFailureHistory exists with this id {id}")
		{
		}
	}
    public class SpecializationNotFoundException : NotFoundException
    {
        public SpecializationNotFoundException(int id) :
            base($"لا يوجد تخصص بهذا الاسم")
            //base($"No Specialization Exists with this id {id}")
        {
        }
    }

}
