﻿namespace Core.Features
{
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
