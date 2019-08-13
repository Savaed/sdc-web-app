using SDCWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SDCWebApp.Services
{
    public interface ISightseeingGroupDbService
    {
        Task<SightseeingGroup> GetGroupAsync(string id);
        Task<IEnumerable<SightseeingGroup>> GetGroupsAsync(IEnumerable<string> ids);
        Task<IEnumerable<SightseeingGroup>> GetAllGroupsAsync();
        Task<IEnumerable<SightseeingGroup>> GetGroupsByAsync(Expression<Func<SightseeingGroup, bool>> predicate);
        Task<SightseeingGroup> AddGroupAsync(SightseeingGroup sightseeingGroup);
        Task<SightseeingGroup> UpdateGroupAsync(SightseeingGroup sightseeingGroup);
        Task DeleteGroupAsync(string id);
    }
}