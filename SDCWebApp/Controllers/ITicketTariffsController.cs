using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;

namespace SDCWebApp.Controllers
{
    public interface ITicketTariffsController
    {
        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/> from parent <see cref="SightseeingTariff"/>
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="sightseeingTariffId">The <see cref="SightseeingTariff"/> parent id.</param>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        Task<IActionResult> GetAllTicketTariffsFromSightseeingTariffAsync(string sightseeingTariffId);

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> to parent <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="TicketTariff"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="TicketTariff">The <see cref="TicketTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An added <see cref="TicketTariffDto"/>.</returns>
        Task<IActionResult> AddTicketTariffToSightseeingTariffAsync(string sightseeingTariffId, [FromBody] TicketTariffDto TicketTariff);

        /// <summary>
        /// Asynchronously deletes specified <see cref="TicketTariff"/> by <paramref name="ticketTariffId"/> from <see cref="SightseeingTariff"/> parent.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="TicketTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        Task<IActionResult> DeleteTicketTariffFromSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId);

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        Task<IActionResult> GetAllTicketTariffsAsync();

        /// <summary>
        /// Asynchronously gets specified <see cref="TicketTariff"/> by <paramref name="id"/> from <see cref="SightseeingTariff"/> parent.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="TicketTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="TicketTariff"/>.</returns>
        Task<IActionResult> GetTicketTariffFromSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId);

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> in parent <see cref="SightseeingTariff"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The id of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="sightseeingTariffId">The id of <see cref="SightseeingTariff"/> which is the parent for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An updated <see cref="TicketTariff"/>.</returns>
        Task<IActionResult> UpdateTicketTariffInSightseeingTariffAsync(string sightseeingTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff);
    }
}