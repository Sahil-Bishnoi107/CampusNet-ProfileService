using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Entities
{
    public class Report
    {
        public string id { get; private set; }
        public string reporterId { get; private set; }
        public string reportedId { get; private set; }

        public string reportedName { get;private set; }
        public string reason { get; private set; }
        Report() { }
        public Report(string reporterId, string reportedId, string reason,string reportedName)
        {
            this.id = Guid.NewGuid().ToString();
            this.reporterId = reporterId;
            this.reportedId = reportedId;
            this.reason = reason;
            this.reportedName = reportedName;
        }
    }
}
