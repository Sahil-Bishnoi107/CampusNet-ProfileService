using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application.Contracts
{
    public record class ProfileUpdate(
            string? userName,
            string? bio,
            string? rollNumber,
            string? collegeEmail,
            string? gitHubLink,
            string? linkedInLink,
            string? profilePictureUrl,
            string? branch,
            string? degree
        );
}
