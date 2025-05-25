using Microsoft.AspNetCore.Mvc;
using StoreApi.Models;
using StoreApi.Repositories;

[ApiController]
[Route("[controller]")]
public class ProductsController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await unitOfWork.Products.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        await unitOfWork.Products.AddAsync(product);
        await unitOfWork.CompleteAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id) return BadRequest();
        unitOfWork.Products.Update(product);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id);
        if (product is null) return NotFound();
        unitOfWork.Products.Delete(product);
        await unitOfWork.CompleteAsync();
        return NoContent();
    }
}