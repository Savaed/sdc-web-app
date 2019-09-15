using Microsoft.AspNetCore.Mvc;
using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using System.Threading.Tasks;
using SDCWebApp.Services;
using System;
using System.Net;
using System.Collections.Generic;

namespace SDCWebApp.Controllers
{
    public interface ITicketTariffsController
    {
        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/> from <see cref="VisitTariff"/> that contain them.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="visitTariffsId">The <see cref="VisitTariff"/> parent ID.</param>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        Task<IActionResult> GetVisitsTicketTariffsAsync(string visitTariffId);

        /// <summary>
        /// Asynchronously adds <see cref="TicketTariff"/> to <see cref="VisitTariff"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="TicketTariff"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariff">The <see cref="TicketTariffDto"/> tariff to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <param name="visitTariffsId">The ID of <see cref="VisitTariff"/> which contains <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An added <see cref="TicketTariffDto"/>.</returns>
        Task<IActionResult> AddVisitsTicketTariffAsync(string visitTariffId, [FromBody] TicketTariffDto ticketTariff);

        /// <summary>
        /// Asynchronously deletes specified <see cref="TicketTariff"/> by <paramref name="ticketTariffId"/> from <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> delete succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="TicketTariff"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffsId">The ID of <see cref="VisitTariff"/> which owns <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        Task<IActionResult> DeleteVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId);

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{TicketTariff}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{TicketTariff}"/>.</returns>
        Task<IActionResult> GetAllTicketTariffsAsync();

        /// <summary>
        /// Asynchronously gets specified <see cref="TicketTariff"/> by <paramref name="id"/> from <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="TicketTariff"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffsId">The ID of <see cref="VisitTariff"/> which owns for <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="TicketTariff"/>.</returns>
        Task<IActionResult> GetVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId);

        /// <summary>
        /// Asynchronously updates <see cref="TicketTariff"/> in <see cref="VisitTariff"/> that owns it.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="TicketTariff"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="TicketTariff"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="ticketTariffId">The ID of <see cref="TicketTariff"/> to be deleted. Cannot be null or empty.</param>
        /// <param name="visitTariffsId">The ID of <see cref="VisitTariff"/> which owns <see cref="TicketTariff"/>. Cannot be null or empty.</param>
        /// <param name="ticketTariff">The <see cref="TicketTariffDto"/> tariff to be updated. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="TicketTariff"/>.</returns>
        Task<IActionResult> UpdateVisitsTicketTariffAsync(string visitTariffId, string ticketTariffId, [FromBody] TicketTariffDto ticketTariff);
    }
}