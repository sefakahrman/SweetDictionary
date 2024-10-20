
using Core.Entities;
using SweetDictionary.Models.Category;

namespace SweetDictionary.Service.Abstract;


public interface ICategoryService
{
    ReturnModel<List<CategoryResponseDto>> GetAll();
    ReturnModel<CategoryResponseDto> GetById(Guid id);
    ReturnModel<CategoryResponseDto> Add(CreateCategoryRequestDto dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequestDto dto);
}
