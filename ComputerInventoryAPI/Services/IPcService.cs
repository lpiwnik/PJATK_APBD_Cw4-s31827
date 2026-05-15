using ComputerInventoryAPI.DTOs;
using ComputerInventoryAPI.Models.Pc;

namespace ComputerInventoryAPI.Services;

public interface IPcService
{
    Task<IEnumerable<PcResponseDto>> GetAllPcsAsync();
    Task<PcWithComponentsResponseDto?> GetPcComponentsAsync(int id);
    Task<PcResponseDto> CreatePcAsync(PcCreateRequestDto dto);
    Task<bool> UpdatePcAsync(int id, PcUpdateRequestDto dto);
    Task<bool> DeletePcAsync(int id);
}