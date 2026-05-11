using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueueManagement.Application.DTOs.Token;
using QueueManagement.Application.Interfaces;
using QueueManagement.Application.Interfaces.Services;
using QueueManagement.Domain.Entities;
using QueueManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace QueueManagement.Application.Services.Token;

public class TokenService : ITokenService
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser>
    _userManager;
    public TokenService( IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<int> GenerateTokenAsync(GenerateTokenDto dto)
    {
        var category = await _unitOfWork.Categories
            .GetByIdAsync(dto.CategoryId);

        if (category == null)
            throw new Exception("Category not found.");

        int nextQueueNumber = 1;

        var existingTokens = await _unitOfWork.Tokens
            .FindAsync(t => !t.IsDeleted);

        if (existingTokens.Any())
        {
            nextQueueNumber = existingTokens
                .Max(t => t.QueueNumber) + 1;
        }

        string tokenNumber =
            $"{category.Prefix}{nextQueueNumber:D3}";

        var token = new Domain.Entities.Token
        {
            TokenNumber = tokenNumber,
            QueueNumber = nextQueueNumber,
            CategoryId = dto.CategoryId,
            SubCategoryId = dto.SubCategoryId,
            CustomerName = dto.CustomerName,
            MobileNumber = dto.MobileNumber,
            Email = dto.Email,
            Status = TokenStatus.Waiting,
            GeneratedAt = DateTime.UtcNow
        };

        await _unitOfWork.Tokens.AddAsync(token);

        await _unitOfWork.SaveChangesAsync();
        return token.Id;
    }

    public async Task<IEnumerable<TokenDto>> GetPendingTokensAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Waiting
                     && !t.IsDeleted,
                "Category",
                "SubCategory");

        return tokens.Select(t => new TokenDto
        {
            Id = t.Id,
            TokenNumber = t.TokenNumber,
            QueueNumber = t.QueueNumber,
            CategoryName = t.Category.Name,
            SubCategoryName = t.SubCategory != null
                ? t.SubCategory.Name
                : null,
            Status = t.Status,
            CustomerName = t.CustomerName,
            GeneratedAt = t.GeneratedAt
        });
    }

    public async Task<IEnumerable<TokenDto>>GetServingTokensAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Serving,
                "Category",
                "SubCategory",
                "Counter");

        return tokens.Select(t => new TokenDto
        {
            Id = t.Id,

            TokenNumber = t.TokenNumber,

            QueueNumber = t.QueueNumber,

            CategoryName = t.Category.Name,

            SubCategoryName =
                t.SubCategory?.Name,

            Status = t.Status,

            CustomerName =
                t.CustomerName,

            GeneratedAt =
                t.GeneratedAt,

            CounterName =
                t.Counter?.Name
        });
    }
    public async Task<IEnumerable<TokenDto>> GetCompletedTokensAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Completed,
                "Category",
                "SubCategory");

        return tokens.Select(t => new TokenDto
        {
            Id = t.Id,
            TokenNumber = t.TokenNumber,
            QueueNumber = t.QueueNumber,
            CategoryName = t.Category.Name,
            SubCategoryName = t.SubCategory?.Name,
            Status = t.Status,
            CustomerName = t.CustomerName,
            GeneratedAt = t.GeneratedAt
        });
    }
    public async Task<TokenDto?>CallNextTokenAsync(string userId)
    {
        var alreadyServing = await _unitOfWork.Tokens
            .FindAsync(t => t.Status == TokenStatus.Serving);

        if (alreadyServing.Any())
        {
            throw new Exception(
                "A token is already being served.");
        }

        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Waiting,
                "Category",
                "SubCategory",
                "Counter");

        var nextToken = tokens
            .OrderBy(t => t.QueueNumber)
            .FirstOrDefault();

        if (nextToken == null)
            return null;

        // Logged in staff

        var user = await _userManager.Users
            .FirstOrDefaultAsync(
                u => u.Id == userId);

        if (user == null)
        {
            throw new Exception(
                "User not found.");
        }

        if (user.CounterId == null)
        {
            throw new Exception(
                "Counter not assigned to staff.");
        }

        // Assign counter

        nextToken.CounterId =
            user.CounterId;

        nextToken.AssignedUserId =
            user.Id;

        nextToken.Status =
            TokenStatus.Serving;

        nextToken.CalledAt =
            DateTime.UtcNow;

        nextToken.ServingStartedAt =
            DateTime.UtcNow;

        _unitOfWork.Tokens.Update(nextToken);

        await _unitOfWork.SaveChangesAsync();

        // Reload token with counter

        var updatedToken =
            await _unitOfWork.Tokens
                .GetAllIncludingAsync(
                    t => t.Id == nextToken.Id,
                    "Category",
                    "SubCategory",
                    "Counter");

        var token = updatedToken.FirstOrDefault();

        return new TokenDto
        {
            Id = token!.Id,

            TokenNumber = token.TokenNumber,

            QueueNumber = token.QueueNumber,

            CategoryName = token.Category.Name,

            SubCategoryName =
                token.SubCategory?.Name,

            Status = token.Status,

            CustomerName =
                token.CustomerName,

            GeneratedAt =
                token.GeneratedAt,

            CounterName =
                token.Counter?.Name

        };
    }
    public async Task<IEnumerable<TokenDto>> GetSkippedTokensAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Skipped,
                "Category");

        return tokens.Select(t => new TokenDto
        {
            Id = t.Id,
            TokenNumber = t.TokenNumber,
            CategoryName = t.Category.Name,
            CustomerName = t.CustomerName,
            Status = t.Status
        });
    }

    public async Task<IEnumerable<TokenDto>> GetTransferredTokensAsync()
    {
        var tokens = await _unitOfWork.Tokens
            .GetAllIncludingAsync(
                t => t.Status == TokenStatus.Transferred,
                "Category");

        return tokens.Select(t => new TokenDto
        {
            Id = t.Id,
            TokenNumber = t.TokenNumber,
            CategoryName = t.Category.Name,
            CustomerName = t.CustomerName,
            Status = t.Status
        });
    }
    public async Task CompleteTokenAsync(int tokenId)
    {
        var token = await _unitOfWork.Tokens
            .GetByIdAsync(tokenId);

        if (token == null)
            throw new Exception("Token not found.");

        token.Status = TokenStatus.Completed;

        token.CompletedAt = DateTime.UtcNow;

        _unitOfWork.Tokens.Update(token);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task TransferTokenAsync(TransferTokenDto dto,string userId)
    {
        var token = await _unitOfWork.Tokens.GetByIdAsync(dto.TokenId);

        if (token == null)
            throw new Exception("Token not found.");

        var transfer = new Domain.Entities.TokenTransfer
        {
            TokenId = token.Id,

            FromCategoryId = token.CategoryId,
            FromSubCategoryId = token.SubCategoryId,

            ToCategoryId = dto.ToCategoryId,
            ToSubCategoryId = dto.ToSubCategoryId,

            TransferReason = dto.TransferReason,

            TransferedByUserId = userId
        };

        await _unitOfWork.TokenTransfers
            .AddAsync(transfer);

        token.CategoryId = dto.ToCategoryId;

        token.SubCategoryId = dto.ToSubCategoryId;

        // IMPORTANT FIX
        token.Status = TokenStatus.Waiting;

        _unitOfWork.Tokens.Update(token);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task<TokenReceiptDto>GetReceiptAsync(int tokenId)
    {
        var token = await _unitOfWork.Tokens.GetAllIncludingAsync(t => t.Id == tokenId,"Category");

        var item = token.FirstOrDefault();

        if (item == null)
            throw new Exception("Token not found.");

        return new TokenReceiptDto
        {
            TokenNumber = item.TokenNumber,

            CustomerName = item.CustomerName,

            CategoryName = item.Category.Name,

            GeneratedAt = item.GeneratedAt
        };
    }
    public async Task RecallTokenAsync(int tokenId)
    {
        var token = await _unitOfWork.Tokens.GetByIdAsync(tokenId);

        if (token == null)
            throw new Exception("Token not found.");

        token.Status = TokenStatus.Waiting;

        _unitOfWork.Tokens.Update(token);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task ReOpenTokenAsync(int tokenId)
    {
        var token = await _unitOfWork.Tokens.GetByIdAsync(tokenId);

        if (token == null)
            throw new Exception("Token not found.");

        token.Status = TokenStatus.Waiting;

        token.CompletedAt = null;

        _unitOfWork.Tokens.Update(token);

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task SkipTokenAsync(int tokenId)
    {
        var token = await _unitOfWork.Tokens.GetByIdAsync(tokenId);

        if (token == null)
            throw new Exception("Token not found.");

        token.Status = TokenStatus.Skipped;

        token.IsSkipped = true;

        _unitOfWork.Tokens.Update(token);

        await _unitOfWork.SaveChangesAsync();
    }
}