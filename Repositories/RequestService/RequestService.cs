using Microsoft.EntityFrameworkCore;
using MyApiProject.Context;
using MyApiProject.Interface;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Repositories.RequestService;

public class RequestService(SqlDbContext dbContext) : IRequestInterface
{
    private readonly SqlDbContext _dbcontext = dbContext;

    public async Task<HttpModel> CreateAsync(RequestDto requestDto)
    {
        try
        {
            var model = new HttpModel
            {
                CollectionId = requestDto.CollectionId,
                Name = requestDto.Name,
                Method = requestDto.Method,
                Url = requestDto.Url

            };
            _dbcontext.Add(model);
            await _dbcontext.SaveChangesAsync();
            return model;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "Internal error");
        }
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        try
        {
            var res = await _dbcontext.HttpModels.FindAsync(Id) ?? throw new Exception("Invalid Id");
            _dbcontext.Remove(res);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "Internal error");
        }
    }


    public async Task<IEnumerable<HttpModel>> GetAllSync()
    {
        try
        {
            var res = await _dbcontext.HttpModels.ToListAsync();
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "internal server error");
        }
    }

    public async Task<HttpModel> GetByCollectionId(Guid CollectionId)
    {
        try
        {
            var res = await _dbcontext.HttpModels.FirstOrDefaultAsync(h => h.CollectionId == CollectionId) ?? throw new KeyNotFoundException($"HttpModel with collection id '{CollectionId}' not found.");
            return res;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " internal server error");
        }
    }

    public async Task<HttpModel?> UpdateAsync(Guid id, UpdateHttpModelDto request)
    {
        try
        {
            var existing = await _dbcontext.HttpModels.FindAsync(id)
                ?? throw new KeyNotFoundException($"HttpModel with id '{id}' not found.");

            // Update properties on the tracked entity
            existing.Name = request.Name;
            existing.Method = request.Method;
            existing.Url = request.Url;
            existing.Headers = request.Headers;
            existing.QueryParams = request.QueryParams;
            existing.Body = request.Body;
            existing.BodyType = request.BodyType;
            existing.AuthType = request.AuthType;
            existing.AuthValue = request.AuthValue;
            existing.UpdatedAt = DateTime.UtcNow;

            await _dbcontext.SaveChangesAsync();
            return existing;
        }
        catch (Exception ex)
        {
            throw new Exception("Internal error: " + ex.Message);
        }
    }

public async Task<RequestHistory> CreateRequestHistoryAsync(RequestHistory history)
{
    try
    {
        if (history == null)
            throw new ArgumentNullException(nameof(history));

        _dbcontext.Add(history);
        await _dbcontext.SaveChangesAsync();
        return history;
    }
    catch (Exception ex)
    {
        throw new Exception("Internal error: " + ex.Message, ex);
    }
}
}
