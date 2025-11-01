using Microsoft.AspNetCore.Mvc;
using MyApiProject.Interface;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Controller;

[ApiController]
[Route("api/[controller]")]
public class RequestController(IRequestInterface requestInterface) : ControllerBase
{
    private readonly IRequestInterface _request = requestInterface;



    [HttpPost]
    public async Task<IActionResult> Create(RequestDto requestDto)
    {
        var res = await _request.CreateAsync(requestDto);
        return Ok(res);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _request.GetAllSync();
        return Ok(res);
    }
    [HttpGet("{CollectionId}")]
    public async Task<IActionResult> GetById(Guid CollectionId)
    {
        var res = await _request.GetByCollectionId(CollectionId);
        return Ok(res);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateHttpModelDto request)
    {
        var res = await _request.UpdateAsync(id, request);
        return Ok(res);

    }
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var res = await _request.DeleteAsync(Id);
        return Ok("Request Deleted SucessFully!");
    }
}
