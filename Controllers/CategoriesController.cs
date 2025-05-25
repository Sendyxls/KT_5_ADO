using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreApi.Repositories;

[ApiController]
[Route("[controller]")]
public class CategoriesController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await unitOfWork.Categories.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        return category is null ? NotFound() : Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        await unitOfWork.Categories.AddAsync(category);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if (id != category.Id) return BadRequest();
        unitOfWork.Categories.Update(category);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await unitOfWork.Categories.GetByIdAsync(id);
        if (category is null) return NotFound();
        unitOfWork.Categories.Delete(category);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}