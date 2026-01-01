using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Entities
{
    public class Profile
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public string? UserName { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public string? Bio { get; private set; }

        public string? RollNumber { get; private set; }

        public string? CollegeEmail { get; private set; }

        public string? GitHubLink { get; private set; }

        public string? LinkedInLink { get; private set; }

        public string? ProfilePictureUrl { get; private set; }

        public string? Branch { get; private set; }
        public string? Degree { get; private set; }
        public string? PhoneNo { get; private set; }

        public Profile(string id, string name, string email,string phoneNo) {
        
            Id = id;
            Name = name;
            Email = email;
            PhoneNo = phoneNo;
            CreatedAt = DateTime.UtcNow;

        }
        public void UpdateProfile(
            string? userName = null,
            string? bio = null,
            string? rollNumber = null,
            string? collegeEmail = null,
            string? gitHubLink = null,
            string? linkedInLink = null,
            string? profilePictureUrl = null,
            string? branch = null,
            string? degree = null
        )
        {
            if (userName != null) UserName = userName;
            if (bio != null) Bio = bio;
            if (rollNumber != null) RollNumber = rollNumber;
            if (collegeEmail != null) CollegeEmail = collegeEmail;
            if (gitHubLink != null) GitHubLink = gitHubLink;
            if (linkedInLink != null) LinkedInLink = linkedInLink;
            if (profilePictureUrl != null) ProfilePictureUrl = profilePictureUrl;
            if (branch != null) Branch = branch;
            if (degree != null) Degree = degree;
        }


    }
}
