using AutoMapper;
using MomPosApi.Models;
using MomPosApi.Services;

public class CategoryService : ICategoryService {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper) {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync() {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int id) {
        var category = await _categoryRepository.GetByIdAsync(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> AddAsync(CategoryDto dto) {
        var category = _mapper.Map<Category>(dto);
        var addedCategory = await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryDto>(addedCategory);
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto dto) {
        var category = _mapper.Map<Category>(dto);
        var updatedCategory = await _categoryRepository.UpdateAsync(category);
        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task<bool> DeleteAsync(int id) {
        return await _categoryRepository.DeleteAsync(id);
    }
}
