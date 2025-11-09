using Microsoft.AspNetCore.Mvc;
using MyApiProject.Interface;
using MyApiProject.Model;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;
using Newtonsoft.Json;

namespace MyApiProject.Controller;

[ApiController]
[Route("api/[controller]")]
public class ProxyController(IProxyInterface proxy, IRequestInterface requestInterface) : ControllerBase
{
    private readonly IProxyInterface _proxy = proxy;
    private readonly IRequestInterface _requestInterface = requestInterface;

    [HttpPost("send")]
    public async Task<ActionResult<HttpResponseDto>> SendRequest([FromBody] SendRequestDto requestDto)
    {
        try
        {
            var response = await _proxy.SendRequestAsync(requestDto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // POST: api/proxy/send/{httpModelId}
    // Send a saved request by ID and save to history
    [HttpPost("send/{httpModelId}")]
    public async Task<ActionResult<HttpResponseDto>> SendSavedRequest(Guid httpModelId)
    {
        try
        {
            var httpmodel = await _requestInterface.GetByCollectionId(httpModelId);
            if (httpmodel == null)
                return NotFound(new { message = "Request not found" });

            // Send the request
            var response = await _proxy.SendRequestAsync(httpmodel);

            // Save the request history
            var history = new RequestHistory
            {
                HttpModelId = httpModelId,
                ResponseBody = response.Body,
                StatusCode = response.StatusCode,
                ResponseHeaders = JsonConvert.SerializeObject(response.Headers),
                RequestedAt = DateTime.UtcNow
            };
            
            await _requestInterface.CreateRequestHistoryAsync(history);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}