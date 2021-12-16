using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace nhH60Services.Models {
    public partial class AspNetUserRole {


        [NotMapped]
        private readonly H60Assignment2DB_nhContext _context;


        public AspNetUserRole() {
            _context = new H60Assignment2DB_nhContext();
        }

        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRole Role { get; set; }
        public virtual AspNetUser User { get; set; }


        //public async Task<AspNetUserRole> GetUserRole(string id) {

        //}
    }
}
