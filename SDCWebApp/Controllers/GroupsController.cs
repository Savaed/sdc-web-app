using Autofac.Features.Indexed;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs proccessing on <see cref="SightseeingGroup"/> entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : CustomApiController, IGroupsController
    {
        private readonly IIndex<string, IServiceBase> _dbServiceFactory;
        private readonly ILogger<GroupsController> _logger;
        private readonly IMapper _mapper;


        public GroupsController(IIndex<string, IServiceBase> dbServiceFactory, ILogger<GroupsController> logger, IMapper mapper) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _dbServiceFactory = dbServiceFactory;
        }


        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{SightseeingGroup}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{SightseeingGroup}"/>.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllGroupsAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllGroupsAsync)}'.");

            ISightseeingGroupDbService groupDbService = null;

            try
            {
                groupDbService = _dbServiceFactory[nameof(ISightseeingGroupDbService)] as ISightseeingGroupDbService;
                var groups = await groupDbService.GetAllAsync();
                var groupDtos = MapToDtoEnumerable(groups);
                var response = new ResponseWrapper(groupDtos);
                _logger.LogInformation($"Finished method '{nameof(GetAllGroupsAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, groupDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="SightseeingGroup"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="SightseeingGroup"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="SightseeingGroup"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searched <see cref="SightseeingGroup"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="SightseeingGroup"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGroupAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetGroupAsync)}'.");

            if (string.IsNullOrEmpty(id))
            {
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");
            }

            ISightseeingGroupDbService groupDbService = null;

            try
            {
                groupDbService = _dbServiceFactory[nameof(ISightseeingGroupDbService)] as ISightseeingGroupDbService;
                var group = await groupDbService.GetAsync(id);
                var groupDto = MapToDto(group);
                var response = new ResponseWrapper(groupDto);
                _logger.LogInformation($"Finished method '{nameof(GetGroupAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(SightseeingGroup).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(ex, groupDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets available sightseeing dates and places number from now to date specified in <see cref="VisitInfo"/> information.
        /// Returns <see cref="HttpStatusCode.OK"/> regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{GroupInfo}"/> that contains available sightseeing dates.</returns>
        [HttpGet("available-dates")]
        public async Task<IActionResult> GetAvailableGroupDatesAsync()
        {
            VisitInfo recentInfo = null;
            IVisitInfoDbService infoDbService = null;
            ISightseeingGroupDbService groupDbService = null;
            List<GroupInfo> availableDates = new List<GroupInfo>();

            try
            {
                infoDbService = _dbServiceFactory[nameof(IVisitInfoDbService)] as IVisitInfoDbService;
                groupDbService = _dbServiceFactory[nameof(ISightseeingGroupDbService)] as ISightseeingGroupDbService;

                recentInfo = await GetRecentSightseeingInfoAsync(infoDbService);

                if (!IsSightseeingDurationSet(recentInfo))
                {
                    _logger.LogWarning($"{nameof(VisitInfo.SightseeingDuration)} is set to 0 hours.");
                    return Ok(new ResponseWrapper(availableDates));
                }

                // Calculate the latest date when you can still buy a ticket. MaxTicketOrderInterval is in weeks.
                var maxTicketPurchaseDate = DateTime.Now.AddDays(recentInfo.MaxTicketOrderInterval * 7);
                var dateTime = DateTime.Now;

                var futureGroups = await groupDbService.GetByAsync(x => x.SightseeingDate > DateTime.Now);

                // Create available sightseeing dates.
                while (dateTime <= maxTicketPurchaseDate)
                {
                    availableDates.AddRange(GetDailyDates(recentInfo, dateTime, futureGroups));
                    dateTime = dateTime.AddDays(1);
                }

                var response = new ResponseWrapper(availableDates);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, $"Element '{nameof(VisitInfo)}' not found.");
                return Ok(new ResponseWrapper(availableDates));
            }
            catch (InternalDbServiceException ex) when (recentInfo is null)
            {
                // Exception thrown by IVisitInfoDbService instance.
                LogInternalDbServiceException(ex, infoDbService.GetType());
                throw;
            }
            catch (InternalDbServiceException ex) when (recentInfo != null)
            {
                // Exception thrown by ISightseeingGroupDbService instance.
                LogInternalDbServiceException(ex, groupDbService.GetType());
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private bool IsSightseeingDurationSet(VisitInfo info)
        {
            return info.SightseeingDuration > 0.0f;
        }

        private async Task<VisitInfo> GetRecentSightseeingInfoAsync(IVisitInfoDbService infoDbService)
        {
            var allInfo = await infoDbService.GetAllAsync();
            return allInfo.OrderByDescending(x => x.UpdatedAt == DateTime.MinValue ? x.CreatedAt : x.UpdatedAt).First();
        }

        private IList<GroupInfo> GetDailyDates(VisitInfo info, DateTime dateTime, IEnumerable<SightseeingGroup> futureGroups)
        {
            List<GroupInfo> availableDates = new List<GroupInfo>();
            var sightseeingDuration = info.SightseeingDuration;
            var openingDateTime = info.GetOpeningDateTime(dateTime);
            var openingHoursNumber = info.GetClosingDateTime(dateTime) - openingDateTime;
            int groupsPerDay = (int)(openingHoursNumber.Hours / sightseeingDuration);

            // Get available sightseeing dates for one day.
            for (int i = 0; i < groupsPerDay; i++)
            {
                GroupInfo groupInfo = new GroupInfo();
                var groupDateTime = openingDateTime.AddHours(i * sightseeingDuration);

                // Get existed group with the same date as currently processing.
                var existedGroup = futureGroups.Where(z => z.SightseeingDate == groupDateTime);

                if (existedGroup.Count() == 0)
                {
                    groupInfo.AvailablePlace = info.MaxAllowedGroupSize;
                }
                else
                {
                    var group = existedGroup.FirstOrDefault();

                    if (group.IsAvailablePlace)
                    {
                        groupInfo.AvailablePlace = group.MaxGroupSize - group.CurrentGroupSize;
                    }
                    else
                    {
                        // If there is no group with available places, do not add the group and start the next iteration.
                        continue;
                    }
                }

                groupInfo.SightseeingDate = groupDateTime;
                availableDates.Add(groupInfo);
            }

            return availableDates;
        }

        private IEnumerable<SightseeingGroupDto> MapToDtoEnumerable(IEnumerable<SightseeingGroup> groups) => _mapper.Map<IEnumerable<SightseeingGroupDto>>(groups);

        private SightseeingGroupDto MapToDto(SightseeingGroup group) => _mapper.Map<SightseeingGroupDto>(group);

        #endregion

    }
}