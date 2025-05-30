﻿using Core.RepositoryContracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class GateRepo : BaseRepository<Gate>, IGateRepo
    {
        //private readonly FoeMaintainContext context;

        public GateRepo(FoeMaintainContext context) : base(context)
        {
            //this.context = context;
        }
        public bool ChackExistance(string name,int RegionId)
        => context.Gates.Any(r => r.Name.Equals(name)&&r.RegionId.Equals(RegionId));
        public async Task CreateNewGate(Gate gate)
        => await Create(gate);

		public void DeleteGate(Gate gate)
		=>SoftDelete(gate);

		public async Task<IEnumerable<Gate>> GetAllGates(int regionId, bool trackchanges)
        => await FindByCondition(g => g.RegionId.Equals(regionId), trackchanges).ToListAsync();

       // public IQueryable<Gate> GetAllGatesInGeneral(bool trackchanges)
       //=> FindAll(trackchanges);

        //public IQueryable<Department> GetDeparmtentsBasedOnGate(int gate, bool track)
        //=> FindByCondition(g => g.Id.Equals(gate),track).SelectMany(g => g.Departments);

        public async Task<Gate> GetSpecificGate(int regionId,int gateId, bool trackchanges)
        => await FindByCondition(g=> g.Id.Equals(gateId)&& g.RegionId.Equals(regionId),trackchanges).SingleOrDefaultAsync();
    }
}
