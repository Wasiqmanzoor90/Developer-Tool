

using Microsoft.AspNetCore.Mvc;
using MyApiProject.Interface;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Controller;


[ApiController]
[Route("api/[controller]")]
public class CollectionController(ICollection collection) : ControllerBase

{
    private readonly ICollection _collection = collection;

    [HttpPost]
    public async Task<IActionResult> Create(CollectionDto collection1)
    {
        try
        {
            var Coll = await _collection.CreateAsync(collection1);
            return Ok("Collection Created sucessfully");
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message + "Internal error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCollection()
    {
        try
        {
            var res = await _collection.GetAllSync();
            return Ok(res);

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message + "Internal error");
        }


    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> ById(Guid Id)
    {
        try
        {
            if (Guid.Empty == Id)

            {
                return BadRequest("Invalid Id");
            }
            var res = await _collection.GetByIdAsync(Id);
            return Ok(res);

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message + "Internal error");
        }
    }

    [HttpPost("{Id}")]
    public async Task<IActionResult> Delete(Guid Id)
    {
        var res = await _collection.DeleteAsync(Id);
        
        return Ok("deleted sucessfully");
    }
}


