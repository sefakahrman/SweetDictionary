
using Core.Exceptions;
using SweetDictionary.Repository.Repositories.Abstracts;

namespace SweetDictionary.Service.Rules;

public class CategoryBusinessRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryBusinessRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void CategoryIsPresent(int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category is null)
        {
            throw new NotFoundException($"İlgili id'ye göre kategori bulunamadı: {id}");
        }
    }
}
