using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features
{
	public abstract class RequestParameters
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get { return _pageSize; }
			set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
		}

		const int maxPageSize = 50;
		private int _pageSize = 10;
        public string? OrderBy { get; set; }
    }
	public class DeviceRequestParameters : RequestParameters
	{
		public int? RegionId { get; set; }
		public int? GateId { get; set; }
		public int? DeptId { get; set; }
		public int? OfficeId { get; set; }
		public string? SearchTerm { get; set; }
        public string? SearchOptions { get; set; }
    }
	public class MaintainanceRequestParameters : RequestParameters
	{
		public string? SearchTerm { get; set; }
	}
}
