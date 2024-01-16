using ApiDevBP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiDevBP.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected ILogger<BaseController> _logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected ObjectResult GetObjectResult(ResponseResultModel result, string? approvalResponseMessage = null)
        {
            LogResult();

            if (result.Failed)
            {
                return BadRequest(result.GetResult);
            }

            return Ok(new
            {
                description = string.IsNullOrEmpty(approvalResponseMessage) ? "La operación se realizó exitosamente." : approvalResponseMessage,
                responseData = result.GetResult
            });
        }

        protected ObjectResult GetErrorObjectResult(Exception ex)
        {
            _logger.LogError("Error en controller: {message} - {stacktrace}", ex.Message, ex.StackTrace);

            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = "UNEXPECTED_ERROR",
                description = ex.Message
            });
        }

        protected void LogResult()
        {
            _logger.LogInformation("Response: {response}. Trace Identifier: {traceIdentifier}. Features: {features}. Request: {request}. Connection: {connection}",
                HttpContext.Response.ToString(),
                HttpContext.TraceIdentifier,
                HttpContext.Features.ToString(),
                HttpContext.Request.ToString(),
                HttpContext.Connection.ToString());
        }
    }
}