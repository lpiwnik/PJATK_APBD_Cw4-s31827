using ComputerInventoryAPI.DTOs;
using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Models.Pc;
using Microsoft.EntityFrameworkCore;

namespace ComputerInventoryAPI.Services;

public class PcService(AppDbContext dbContext) : IPcService
{
    public async Task<IEnumerable<PcResponseDto>> GetAllPcsAsync() =>
        await dbContext.Pc.Select(pc => new PcResponseDto(
            pc.Id,
            pc.Name,
            pc.Weight,
            pc.Warranty,
            pc.CreatedAt,
            pc.Stock
        )).ToListAsync();

    public async Task<PcWithComponentsResponseDto?> GetPcComponentsAsync(int id) => 
        await dbContext.Pc
            .Where(pc => pc.Id == id)
            .Select(pc => new PcWithComponentsResponseDto(
                pc.Id,
                pc.Name,
                pc.Weight,
                pc.Warranty,
                pc.CreatedAt,
                pc.Stock,
                pc.PcComponents.Select(component => new PcComponentDetailDto(
                    component.Amount,
                    new ComponentDetailDto(
                        component.Component.Code,
                        component.Component.Name,
                        component.Component.Description,
                        new ManufacturerDto(
                            component.Component.Manufacturer.Id,
                            component.Component.Manufacturer.Abbreviation,
                            component.Component.Manufacturer.FullName,
                            component.Component.Manufacturer.FoundationDate.ToString("yyyy-MM-dd")
                        ),
                        new TypeDto(
                            component.Component.Type.Id,
                            component.Component.Type.Abbreviation,
                            component.Component.Type.Name
                            )
                    )
                )).ToList()
            )).FirstOrDefaultAsync();

    

    public async Task<PcResponseDto> CreatePcAsync(PcCreateRequestDto dto)
    {
        var newPc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = DateTime.UtcNow,
            Stock = dto.Stock
        };
        dbContext.Pc.Add(newPc);
        await dbContext.SaveChangesAsync();

        return new PcResponseDto(
            newPc.Id,
            newPc.Name,
            newPc.Weight,
            newPc.Warranty,
            newPc.CreatedAt,
            newPc.Stock
        );
    }

    public async Task<bool> UpdatePcAsync(int id, PcUpdateRequestDto dto)
    {
        var pc = await dbContext.Pc.FirstOrDefaultAsync(pc=> pc.Id == id);
        if (pc == null)
            return false;
        
        pc.Name=dto.Name;
        pc.Weight=dto.Weight;
        pc.Warranty=dto.Warranty;
        pc.CreatedAt=dto.CreatedAt;
        pc.Stock=dto.Stock;
        
        await dbContext.SaveChangesAsync();
        
        return true;
    }
    public async Task<bool> DeletePcAsync(int id)
    {
        var pc = await dbContext.Pc.FirstOrDefaultAsync(pc=> pc.Id == id);
        if (pc == null)
            return false;
        
        dbContext.Pc.Remove(pc);
        
        await dbContext.SaveChangesAsync();

        return true;
    }
}