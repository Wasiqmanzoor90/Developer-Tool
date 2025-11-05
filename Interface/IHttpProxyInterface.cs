using MyApiProject.Model;
using MyApiProject.Model.DTO;
using MyApiProject.Model.Entites;

namespace MyApiProject.Interface;

public interface IProxyInterface
{
    Task<SendRequestDto> SendRequestAsync(SendRequestDto sendRequestDto);
     Task<HttpResponseDto> SendRequestAsync(HttpModel httpModel);
}