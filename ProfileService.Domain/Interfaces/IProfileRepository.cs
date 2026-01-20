using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Domain.Entities;

namespace ProfileService.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task AddProfileAsync(string userId,string name,string email,string phoneNo);

        Task<Profile> GetMyProfile();
        Task<Profile> GetByIdAsync(string id);
        Task UpdateAsync(string? username,string? bio,string? rollno, string? collegeEmail,string? githubLink,string? linkedinLink,string? profilePicLink,string? branch,string? degree);

        Task<bool> ConfirmCollege(string collegeEmail, string otp);

        Task<bool> ConfirmPhoneNo(string phoneNo,string otp);    
    }
}
