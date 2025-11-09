using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Interface;

public interface IRequestInterface
{
     Task<IEnumerable<HttpModel>> GetAllSync();
    Task<HttpModel> GetByCollectionId(Guid CollectionId);
    Task<HttpModel> CreateAsync(RequestDto requestDto);
Task<RequestHistory> CreateRequestHistoryAsync(RequestHistory history); 
    Task<HttpModel?> UpdateAsync(Guid id,UpdateHttpModelDto request);

    Task<bool> DeleteAsync(Guid Id);


    
}