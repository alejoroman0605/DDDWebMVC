using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces {
    public interface IDeveloperRepository : IGenericRepository<Developer> {
        IEnumerable<Developer> GetPopularDevelopers (int count);
        Task Edit (int id, Developer developer);
    }
}