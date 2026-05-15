using ComputerInventoryAPI.DTOs;
using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Models.Pc;
using ComputerInventoryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComputerInventoryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController(IPcService pcService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await pcService.GetAllPcsAsync());

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetPcComponents(int id)
    {
        var pcWithComponents = await pcService.GetPcComponentsAsync(id);

        if (pcWithComponents == null)
        {
            return NotFound(new { Message = $"Computer with ID {id} not found." });
        }
        return Ok(pcWithComponents);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePc(PcCreateRequestDto dto)
    {
        
        var createdPc = await pcService.CreatePcAsync(dto);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = createdPc.Id }, 
            createdPc
        );
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc(int id, PcUpdateRequestDto dto)
    {
        var result = await pcService.UpdatePcAsync(id, dto);
        if (!result)
            return NotFound($"Pc with ID {id} not found.");
        return Ok($"Pc with ID {id} updated.");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc(int id)
    {
        var result = await pcService.DeletePcAsync(id);
        if (!result)
            return NotFound($"Pc with ID {id} not found.");
        return NoContent();
    }
    
    
}