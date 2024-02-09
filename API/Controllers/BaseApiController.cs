using API.Setup.Extensions;
using API.Setup.Filters;
using Application.Core;
using Asp.Versioning;
using Domain.Core;
using Domain.User;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
	/// <summary>
	/// Base class with general methods for interacting with responses
	/// 
	/// <summary>
	/// This is the base API controller. It sets the API version to 1.0 and defines the route for the controller.
	/// </summary>
	/// <remarks>
	/// The API version can be overridden in derived controllers using the MapToApiVersion attribute.
	/// This will only restrict a particular method
	/// The APIVersion has to be on all controllers
	/// Controllers can share the same namespace but to be grouped together, should follow the format of [Name][#][Controller], as below
	/// This will help on needing the to bare separate versions
	/// </remarks>
	// [ApiVersion("1.0")]
	// [ApiVersion("3.0")]
	// public class HelloWorldController : BaseApiController
	// {
	//     [HttpGet]
	//     public string Get() => "Hello world v1!";
	//
	//     [HttpGet]
	//     [MapToApiVersion("3.0")]
	//     public string GetV3() => "Hello world v3!";
	// }
	// [ApiVersion("4.0")]
	// public class HelloWorld2Controller : BaseApiController
	// {
	//     [HttpGet]
	//     public string Get() => "Hello world v4!";
	// }
	
	/// </summary>
	[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
	[ServiceFilter(typeof(LogUserActionFilter))]
    public class BaseApiController : ControllerBase
	{
		private IMediator? _mediator;
		protected IMediator Mediator
		{
			get
			{
				IMediator? mediator = (_mediator ??= HttpContext.RequestServices.GetService<IMediator>()) ?? throw new ArgumentNullException("mediator");
                return mediator;
			}
		}

		protected string? CurrentUser
		{
			get
			{

				return User.Identity?.Name;
			}
		}

		/// <summary>
		/// Wrap around the Application response and send back code depending on contents 
		/// </summary>
		/// <param name="result">Application layer response</param>
		/// <typeparam name="T">Type of data</typeparam>
		/// <returns>ActionResult</returns>
		protected ActionResult HandleResult<T>(Result<T> result)
		{
			if (
			 !result.IsSuccess
			 && result.Error != null
			 && result.Error.Equals("NotFound")
			) return NotFound();

			if (result.PartialSuccessMessages != null)
			{
				return Ok(new { result.Value, result.PartialSuccessMessages });
			}

			if (result.IsSuccess && result.Value != null)
				return Ok(result.Value);
			if (result.IsSuccess && result.Value == null)
				return NotFound();

			var details = new ProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Title = "An error occurred while processing your request",
				Detail = result.Error,
			};

			return new BadRequestObjectResult(details);
		}
		/// <summary>
		/// Wrap around a PagedList Application response and send back code depending on contents
		/// </summary>
		/// <param name="result">Application layer paged response</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		protected ActionResult HandlePagedResult<T>(Result<PagedList<T>> result)
		{
			if (
			 !result.IsSuccess
			 && result.Error != null
			 && result.Error.Equals("NotFound")
			) return NotFound();

			if (result.IsSuccess && result.Value != null)
			{
				Response.AddPaginationHeader(result.Value.CurrentPage, result.Value.PageSize,
						result.Value.TotalCount, result.Value.TotalPages);
				return Ok(result.Value);

			}
			if (result.IsSuccess && result.Value == null)
				return NotFound();

			var details = new ProblemDetails
			{
				Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
				Title = "An error occurred while processing your request",
				Detail = result.Error,
			};

			return new BadRequestObjectResult(details);
		}

		protected ValidationResult ValidateCommand<TCommand, TValidator>(TCommand command)
where TValidator : AbstractValidator<TCommand>, new()
		{
			TValidator validator = new();
			ValidationResult validationResult = validator.Validate(command);
			return validationResult;
		}
	}
}