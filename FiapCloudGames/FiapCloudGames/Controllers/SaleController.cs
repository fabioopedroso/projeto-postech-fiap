using Application.DTOs.Sale.Signature;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FiapCloudGamesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Administrator")]
public class SaleController : ControllerBase
{
    private readonly ISaleAppService _saleAppService;

    public SaleController(ISaleAppService saleAppService)
    {
        _saleAppService = saleAppService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleDto dto)
    {
        var sale = await _saleAppService.Create(dto);
        return Ok(sale);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateSale([FromBody] UpdateSaleDto dto)
    {
        await _saleAppService.Update(dto);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSales()
    {
        var sales = await _saleAppService.GetAll();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSaleById(int id)
    {
        var sale = await _saleAppService.GetbyId(id);
        return Ok(sale);
    }

    [HttpPost("AddGameToSale")]
    public async Task<IActionResult> AddGameToSale([FromBody] UpdateSaleGameDto dto)
    {
        await _saleAppService.AddGameToSale(dto);
        return NoContent();
    }

    [HttpDelete("RemoveGameFromSale")]
    public async Task<IActionResult> RemoveGameFromSale([FromBody] UpdateSaleGameDto dto)
    {
        await _saleAppService.RemoveGameFromSale(dto);
        return NoContent();
    }
}
