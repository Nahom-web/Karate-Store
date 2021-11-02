using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace nhH60Services.Models {
    public partial class Role {

        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;

        public Role() {
            _context = new H60Assignment2DB_nhContext();
        }


        public int Id { get; set; }
        public string Name { get; set; }



        public async Task<List<Role>> GetAllRoles() {
            return await _context.Roles.OrderBy(pc => pc.Name).ToListAsync();
        }
    }
}
