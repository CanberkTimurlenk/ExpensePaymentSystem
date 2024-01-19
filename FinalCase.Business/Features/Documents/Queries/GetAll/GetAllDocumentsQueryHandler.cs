using AutoMapper;
using AutoMapper.QueryableExtensions;
using FinalCase.Base.Response;
using FinalCase.Data.Contexts;
using FinalCase.Schema.Entity.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinalCase.Business.Features.Documents.Queries.GetAll;
public class GetAllDocumentsQueryHandler(FinalCaseDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllDocumentsQuery, ApiResponse<IEnumerable<DocumentResponse>>>
{
    private readonly IMapper mapper = mapper;
    private readonly FinalCaseDbContext dbContext = dbContext;

    public async Task<ApiResponse<IEnumerable<DocumentResponse>>> Handle(GetAllDocumentsQuery request, CancellationToken cancellationToken)
    {
        var document = await dbContext.Documents
                                .Include(d => d.Expense)
                                .ProjectTo<DocumentResponse>(mapper.ConfigurationProvider)
                                .AsNoTrackingWithIdentityResolution() // disable tracking
                                .ToListAsync(cancellationToken);

        var response = mapper.Map<IEnumerable<DocumentResponse>>(document);

        return new ApiResponse<IEnumerable<DocumentResponse>>(response);
    }
}
