using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);

         Task<bool>SaveAllAsync();

         Task<IEnumerable<AppUser>>GetUsersAsync();

         Task<AppUser>GetUserByIdAsync(int id);

         Task<AppUser>GetUserByUsernameAsync(string username);

         Task<PagedList<MemberDto>>GetMembersAsync(UserParams UserParams);

         Task<MemberDto>GetMemberAsync(string username);
    }
}

/*
Update: This method updates the properties of an existing user in the repository.

SaveAllAsync: This method saves all changes made to the repository asynchronously and returns a boolean indicating whether the save was successful.

GetUsersAsync: This method retrieves a collection of all users in the repository asynchronously.

GetUserByIdAsync: This method retrieves a user by their id from the repository asynchronously.

GetUserByUsernameAsync: This method retrieves a user by their username from the repository asynchronously.

GetMembersAsync: This method retrieves a collection of MemberDto objects representing users in the repository asynchronously.

GetMemberAsync: This method retrieves a MemberDto object representing a user by their username from the repository asynchronously.
*/