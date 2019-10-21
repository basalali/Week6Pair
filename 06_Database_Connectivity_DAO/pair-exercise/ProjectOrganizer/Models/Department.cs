using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.Models
{
    public class Department
    {
        /// <summary>
        /// The dept id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The dept name.
        /// </summary>
        public string Name { get; set; }

        public override string ToString()
        {
            return Id.ToString().PadRight(6) + Name.PadRight(30);
        }
    }
}
