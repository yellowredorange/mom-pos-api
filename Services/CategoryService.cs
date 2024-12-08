using AutoMapper;
using MomPosApi.Models;
using MomPosApi.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IRepository<Category> categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        try
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all categories");
            throw;
        }
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogWarning("Category with id {CategoryId} not found", id);
                throw new KeyNotFoundException($"Category with id {id} not found");
            }
            return _mapper.Map<CategoryDto>(category);
        } catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Category with id {CategoryId} not found", id);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the category");
            throw;
        }
    }

    public async Task<CategoryDto> AddAsync(CategoryDto dto)
    {
        try
        {
            var category = _mapper.Map<Category>(dto);
            var addedCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDto>(addedCategory);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new category");
            throw;
        }
    }

    public async Task<CategoryDto> UpdateAsync(CategoryDto dto)
    {
        try
        {
            var category = _mapper.Map<Category>(dto);
            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryDto>(updatedCategory);
        } catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Category with id {CategoryId} not found", dto.CategoryId);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the category");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var result = await _categoryRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Category with id {CategoryId} not found", id);
                throw new KeyNotFoundException($"Category with id {id} not found");
            }
            return result;
        } catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Category with id {CategoryId} not found", id);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the category");
            throw;
        }
    }

    public async Task AddRangeAsync(IEnumerable<CategoryDto> dtos)
    {
        try
        {
            var categories = _mapper.Map<IEnumerable<Category>>(dtos);
            await _categoryRepository.AddRangeAsync(categories);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding multiple categories");
            throw;
        }
    }

    public async Task UpdateBatchAsync(IEnumerable<CategoryDto> categoryDtos)
    {
        try
        {
            var categories = _mapper.Map<IEnumerable<Category>>(categoryDtos);
            await _categoryRepository.UpdateBatchAsync(categories);
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating multiple categories");
            throw;
        }
    }
}
