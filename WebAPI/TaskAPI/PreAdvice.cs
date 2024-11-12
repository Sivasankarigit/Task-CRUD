using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.TaskAPI
{
    public class PreAdvice
    {
        public int preAdviceId { get; set; }
        public string depot{ get; set; }
        public string liner{ get; set; }
        public string redelAuthNo{ get; set; }
        public DateOnly redelAuthDate{ get; set; }
        public string vesselCarrier { get; set; }
        public string vesselName { get; set; }
        
    }
}