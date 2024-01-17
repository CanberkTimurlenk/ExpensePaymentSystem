using AutoMapper;
using FinalCase.Base.Response;
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
                                .AsNoTracking() // Since the operation is readonly (query)
                                            // we can use AsNoTracking to improve performance.
                                .FirstOrDefaultAsync(d => d.Id.Equals(request.Id), cancellationToken);

        var response = mapper.Map<DocumentResponse>(document);

        return new ApiResponse<DocumentResponse>(response);
    }
}
