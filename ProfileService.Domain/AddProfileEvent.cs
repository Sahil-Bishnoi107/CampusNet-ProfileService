using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain
{
    public record AddProfileEvent(string UserId,string Email,string Name,string PhoneNo);

}
