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
	}
	public class MaintainanceRequestParameters : RequestParameters
	{
		public string? SearchTerm { get; set; }
	}

	public class PagedList<T> : List<T>
	{
		public MetaData metaData { get; set; }
		public PagedList(IEnumerable<T> source, int totalRecords, int pageNumber, int pageSize)
		{
			metaData = new MetaData()
			{
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalRecords = totalRecords,
				TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize)
			};
			AddRange(source);
		}
		public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
		{
			var totalRecords = source.Count();
			var items = source.
				Skip((pageNumber - 1) * pageSize)
				.Take(pageSize).ToList();

			return new PagedList<T>(items, totalRecords, pageNumber, pageSize);
		}
		
	}
	public class MetaData
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalRecords { get; set; }

		public bool HasPrivous => CurrentPage > 1;
		public bool HasNext => CurrentPage < TotalPages;
	}
}
