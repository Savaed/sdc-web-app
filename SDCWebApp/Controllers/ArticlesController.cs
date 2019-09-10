using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using SDCWebApp.Models;
using SDCWebApp.Models.Dtos;
using SDCWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using SDCWebApp.Helpers.Constants;

namespace SDCWebApp.Controllers
{
    /// <summary>
    /// Provides methods to Http verbs proccessing on <see cref="Article"/> entities.
    /// </summary>
    [Route("api/[controller]")]
    [Authorize(ApiConstants.ApiUserPolicyName)]
    [ApiController]
    public class ArticlesController : CustomApiController, IArticlesController
    {
        private const string ControllerPrefix = "articles";
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleDbService _articleDbService;
        private readonly IMapper _mapper;


        public ArticlesController(IArticleDbService articleDbService, ILogger<ArticlesController> logger, IMapper mapper) : base(logger)
        {
            _articleDbService = articleDbService;
            _logger = logger;
            _mapper = mapper;
        }


        /// <summary>
        /// Asynchronously adds <see cref="Article"/>.
        /// Returns <see cref="HttpStatusCode.Created"/> response if <see cref="Article"/> create succeeded or
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="article">The <see cref="ArticleDto"/> Article to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An added <see cref="ArticleDto"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddArticleAsync([FromBody] ArticleDto article)
        {
            _logger.LogInformation($"Starting method '{nameof(AddArticleAsync)}'.");

            try
            {
                // Ignore Id if the client set it. Id of entity is set internally by the server.
                article.Id = null;

                var articleToBeAdded = MapToDomainModel(article);
                var addedArticle = await _articleDbService.RestrictedAddAsync(articleToBeAdded);

                // Reverse maps only for response to the client.
                var addedArticleDto = MapToDto(addedArticle);
                var response = new ResponseWrapper(addedArticleDto);
                string addedArticleUrl = $"{ControllerPrefix}/{addedArticle.Id}";
                _logger.LogInformation($"Finished method '{nameof(addedArticle)}'.");
                return Created(addedArticleUrl, response);
            }
            catch (InvalidOperationException ex)
            {
                return OnInvalidParameterError($"Element '{typeof(Article).Name}' already exists.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_articleDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously deletes specified <see cref="Article"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="Article"/> delete suceeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> if specified <see cref="Article"/> not found.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="Article"/> to be deleted. Cannot be null or empty.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticleAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(DeleteArticleAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                await _articleDbService.DeleteAsync(id);
                var response = new ResponseWrapper();
                _logger.LogInformation($"Finished method '{nameof(DeleteArticleAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Article).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_articleDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets <see cref="IEnumerable{Article}"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response regardless if returned set is empty or not.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <returns><see cref="IEnumerable{Article}"/>.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllArticlesAsync()
        {
            _logger.LogInformation($"Starting method '{nameof(GetAllArticlesAsync)}'.");

            try
            {
                var articles = await _articleDbService.GetAllAsync();
                var articlesDto = MapToDtoEnumerable(articles);
                var response = new ResponseWrapper(articlesDto);
                _logger.LogInformation($"Finished method '{nameof(GetAllArticlesAsync)}'.");
                return Ok(response);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_articleDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously gets specified <see cref="Article"/> by <paramref name="id"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if the specified <see cref="Article"/> found, 
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Article"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of searching <see cref="Article"/>. Cannot be null or empty.</param>
        /// <returns>An specified <see cref="Article"/>.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetArticleAsync(string id)
        {
            _logger.LogInformation($"Starting method '{nameof(GetArticleAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"Parameter '{nameof(id)}' cannot be null or empty.");

            try
            {
                var article = await _articleDbService.GetAsync(id);
                var articleDto = MapToDto(article);
                var response = new ResponseWrapper(articleDto);
                _logger.LogInformation($"Finished method '{nameof(GetArticleAsync)}'.");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Article).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_articleDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }

        /// <summary>
        /// Asynchronously updates <see cref="Article"/>.
        /// Returns <see cref="HttpStatusCode.OK"/> response if <see cref="Article"/> update succeeded,
        /// <see cref="HttpStatusCode.BadRequest"/> response if the request is malformed or
        /// <see cref="HttpStatusCode.NotFound"/> response if specified <see cref="Article"/> does not exist.
        /// Throws an <see cref="InternalDbServiceException"/> or <see cref="Exception"/> if any internal problem with processing data.
        /// </summary>
        /// <param name="id">The id of <see cref="Article"/> to be updated. Cannot be null or empty. Must match to <paramref name="article"/>.Id property.</param>
        /// <param name="article">The <see cref="Article"/> article to be added. This parameter is a body of JSON request. Cannot be null.</param>
        /// <returns>An updated <see cref="Article"/>.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateArticleAsync(string id, [FromBody] ArticleDto article)
        {
            _logger.LogInformation($"Starting method '{nameof(UpdateArticleAsync)}'.");

            if (string.IsNullOrEmpty(id))
                return OnInvalidParameterError($"An argument '{nameof(id)}' cannot be null or empty.");

            if (!id.Equals(article.Id))
                return OnMismatchParameterError($"An '{nameof(id)}' in URL end field '{nameof(article.Id).ToLower()}' in request body mismatches. Value in URL: '{id}'. Value in body: '{article.Id}'.");

            try
            {
                var articleToBeUpdated = MapToDomainModel(article);
                var updatedArticle = await _articleDbService.RestrictedUpdateAsync(articleToBeUpdated);

                // Revers map for client response.
                article = MapToDto(updatedArticle);
                var response = new ResponseWrapper(article);
                _logger.LogInformation($"Finished method '{nameof(UpdateArticleAsync)}'");
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return OnNotFoundError($"Cannot found element '{typeof(Article).Name}' with specified id: '{id}'.", ex);
            }
            catch (InternalDbServiceException ex)
            {
                LogInternalDbServiceException(_articleDbService.GetType(), ex);
                throw;
            }
            catch (Exception ex)
            {
                LogUnexpectedException(ex);
                throw;
            }
        }


        #region Privates

        private Article MapToDomainModel(ArticleDto articleDto) => _mapper.Map<Article>(articleDto);
        private ArticleDto MapToDto(Article article) => _mapper.Map<ArticleDto>(article);
        private IEnumerable<ArticleDto> MapToDtoEnumerable(IEnumerable<Article> article) => _mapper.Map<IEnumerable<ArticleDto>>(article);

        #endregion

    }
}