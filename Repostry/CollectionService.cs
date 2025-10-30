
using Microsoft.EntityFrameworkCore;
using MyApiProject.Context;
using MyApiProject.Interface;
using MyApiProject.Model.DTOs;
using MyApiProject.Model.Entites;

namespace MyApiProject.Repositories;

public class CollectionService(SqlDbContext dbContext) : ICollection
{

private readonly SqlDbContext _dbcontext = dbContext;

    public async Task<Collection?> CreateAsync(CollectionDto collectionDto)
    {
        try
        {
            var collection = new Collection
            {
                CollectionId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                Name = collectionDto.Name
            };

            // Add the entity to DbContext
            _dbcontext.Collections.Add(collection);
            await _dbcontext.SaveChangesAsync();

            return collection; // Return the created collection
        }
        catch (Exception ex)
        {
            throw new Exception("Server error: " + ex.Message);
        }

    }

    public async Task<IEnumerable<Collection?>> GetAllSync()
    {
        try
        {
            var res = await _dbcontext.Collections.ToListAsync();
            return res;

        }
        catch (Exception ex)
        {

            throw new Exception("Server error: " + ex.Message);

        }
    }
    

    public async Task<Collection?> GetByIdAsync(Guid Id)
    {
        try
        {
            var res = await _dbcontext.Collections.FindAsync(Id);
            return res;
            
        }
        catch (Exception ex)
        {
            
            throw new Exception("Server error: " + ex.Message);   ;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
          try
        {
            var del = await _dbcontext.Collections.FindAsync(id) ?? throw new Exception("Id is missing");
            _dbcontext.Collections.Remove(del);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            
            throw new Exception("Server error: " + ex.Message);   ;
        }
    }





    

}