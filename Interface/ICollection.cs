

using MyApiProject.Model.DTOs;
using MyApiProject.Model.Entites;

namespace MyApiProject.Interface;

public interface ICollection
{
    Task<IEnumerable<Collection?>> GetAllSync();
    Task<Collection?> GetByIdAsync(Guid Id);
    Task<Collection?> CreateAsync(CollectionDto collectionDto);
    Task<bool> DeleteAsync(Guid Id);

}