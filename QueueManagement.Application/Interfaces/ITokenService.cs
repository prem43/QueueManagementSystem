using QueueManagement.Application.DTOs.Token;

namespace QueueManagement.Application.Interfaces.Services;

public interface ITokenService
{
    Task<int> GenerateTokenAsync(GenerateTokenDto dto);

    Task<IEnumerable<TokenDto>> GetPendingTokensAsync();

    Task<IEnumerable<TokenDto>> GetServingTokensAsync();

    Task<IEnumerable<TokenDto>> GetCompletedTokensAsync();

    Task<TokenDto?> CallNextTokenAsync(string userId);

    Task CompleteTokenAsync(int tokenId);
    Task TransferTokenAsync(TransferTokenDto dto,string userId);
    Task<IEnumerable<TokenDto>>GetSkippedTokensAsync();
    Task<TokenReceiptDto>GetReceiptAsync(int tokenId);
    Task RecallTokenAsync(int tokenId);
    Task<IEnumerable<TokenDto>>GetTransferredTokensAsync();
    Task ReOpenTokenAsync(int tokenId);
    Task SkipTokenAsync(int tokenId);
}