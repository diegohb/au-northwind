using System;
using System.Collections.Generic;

namespace Presentation.WebSPA.Scaffolded.Entities
{
    public partial class Territory
    {
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
