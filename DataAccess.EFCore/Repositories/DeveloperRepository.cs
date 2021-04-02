using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories {
    public class DeveloperRepository : GenericRepository<Developer>, IDeveloperRepository {
        public DeveloperRepository (ApplicationContext context) : base (context) { }
        public IEnumerable<Developer> GetPopularDevelopers (int count) {
            return _context.Developers.OrderByDescending (d => d.Followers).Take (count).ToList ();
        }
        public async Task Edit (int id, Developer developer) {
            if (id == developer.Id) {
                try {
                    _context.Developers.Update (developer);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    throw;
                }
            }
        }
    }
}