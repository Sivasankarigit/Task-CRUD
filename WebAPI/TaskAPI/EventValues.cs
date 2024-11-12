using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.TaskAPI
{
    public class EventValues
    {
        public int first { get; set; }
        public int take { get; set; }
        public string? globalFilter { get; set; }

        public string? columnName { get; set; }
        public string? columnValue { get; set; }
    }
}