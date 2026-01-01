using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Interfaces
{
    public interface IJwtRepository
    {
        string GenerateUserId();
    }
}
