using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static BTA.Models.ApplicationDbContext;

namespace BTA.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
        }
    }
}