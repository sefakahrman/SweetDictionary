
using AutoMapper;
using Core.Entities;
using SweetDictionary.Models.Category;
using SweetDictionary.Models.Entities;
using SweetDictionary.Repository.Repositories.Abstracts;
using SweetDictionary.Service.Abstract;
using SweetDictionary.Service.Rules;

namespace SweetDictionary.Service.Concretes;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _businessRules;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules businessRules)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public ReturnModel<CategoryResponseDto> Add(CreateCategoryRequestDto dto)
    {
        Category createdCategory = _mapper.Map<Category>(dto);
        createdCategory.Id = Guid.NewGuid();

        Category category = _categoryRepository.Add(createdCategory);

        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(category);

        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = "Kategori eklendi.",
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.CategoryIsPresent(id);

        Category? category = _categoryRepository.GetById(id);
        Category deletedCategory = _categoryRepository.Delete(category);

        return new ReturnModel<string>
        {
            Data = $"Kategori Adı: {deletedCategory.Name}",
            Message = "Kategori silindi.",
            Status = 204,
            Success = true
        };
    }

    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        var categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);
        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            Status = 200,
            Success = true
        };
    }

    public ReturnModel<CategoryResponseDto> GetById(Guid id)
    {
        try
        {
            _businessRules.CategoryIsPresent(id);

            var category = _categoryRepository.GetById(id);
            var response = _mapper.Map<CategoryResponseDto>(category);
            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "İlgili kategori gösterildi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<CategoryResponseDto>.HandleException(ex);
        }
    }

    public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequestDto dto)
    {
        try
        {
            _businessRules.CategoryIsPresent(dto.Id);

            Category category = _mapper.Map<Category>(dto);
            Category updated = _categoryRepository.Update(category);

            CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(updated);

            return new ReturnModel<CategoryResponseDto>
            {
                Data = response,
                Message = "Kategori güncellendi.",
                Status = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<CategoryResponseDto>.HandleException(ex);
        }
    }
}
