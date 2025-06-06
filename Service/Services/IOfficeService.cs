﻿using Service.DTOs;
namespace Service.Services
{
	public interface IOfficeService 
    {
        Task<IEnumerable<OfficeDto>> GetAll(int regionId,int gateId, int deptId, bool trackchanges);
        Task<OfficeDto> GetOfficeBasedOnId(int regionId, int gateId, int deptId, int officeId, bool trackchanges);
        Task<OfficeDto> CreateNewOffice(int regionId, int gateId, int deptId,string officeName ,bool trackchanges);
        Task DeleteOffice(int regionId, int gateId, int deptId, int officeId, bool trackchanges);

    }
}
