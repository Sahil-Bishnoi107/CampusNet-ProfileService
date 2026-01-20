using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application.Contracts
{
    public record ReviewContract(string ReviewedId, int ReviewScore, string ReviewedName);

}
