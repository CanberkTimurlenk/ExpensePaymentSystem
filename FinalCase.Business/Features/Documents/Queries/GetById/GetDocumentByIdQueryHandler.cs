using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Business.Features.Documents.Constants;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Documents.Queries.GetById;
public class GetDocumentByIdQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetDocumentByIdQuery, ApiResponse<DocumentResponse>>
{
    private readonly IMapper mapper = mapper;
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse<DocumentResponse>> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await dbContext.Documents
                                .Include(d => d.Expense)
                                .Where(d => d.Id.Equals(request.Id))
                                .ProjectTo<DocumentResponse>(mapper.ConfigurationProvider)
                                .AsNoTracking() // Since the operation is readonly (query)
                                                // we can use AsNoTracking to improve performance.
                                .FirstOrDefaultAsync(cancellationToken);

        if (document == null)
            return new ApiResponse<DocumentResponse>(DocumentMessages.DocumentNotFound);

        var response = mapper.Map<DocumentResponse>(document);

        return new ApiResponse<DocumentResponse>(response);
    }
}
