using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Minenetred.Web.Context.ContextModels
{
    public class Log
    {
        [Required]
        public Guid LogId { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
    }
}
