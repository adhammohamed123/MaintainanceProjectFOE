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
        public DeviceNotFoundException(int id) : base($"Device with Id {id} not found in our App")
        {
        }
    }

    public class RegionNotFoundException : NotFoundException
    {
        public RegionNotFoundException(int id) : base($"Sorry no Regions Registered with this Id {id}")
        {
        }
    }

    public class GateNotFoundException : NotFoundException 
    {
        public GateNotFoundException(int id):base($"No Gate Available With This Id {id}")
        {
            
        }
    }

    public class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException(int id):base($"no Department exists with this id {id} for that specific Gate")
        {
            
        }
    }
    public class OfficeNotFoundException : NotFoundException
    {
        public OfficeNotFoundException(int id) : base($"no office exists with this id {id}")
        {
        }
    }
	public class FailureNotFoundException : NotFoundException
	{
		public FailureNotFoundException(int id) : base($"no Failure exists with this id {id}")
		{
		}
	}
	public class StuffNotFoundException : NotFoundException
	{
		public StuffNotFoundException(int id) : base($"no Stuff exists with this id {id}")
		{
		}
	}
    public class DeviceFailureHistoryNotFoundException:NotFoundException
    {
		public DeviceFailureHistoryNotFoundException(int id) : base($"no DeviceFailureHistory exists with this id {id}")
		{
		}
	}

}
