using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Commands
{
    public record UpdateReviewCommand(string id,int score) : IRequest;
    
}
