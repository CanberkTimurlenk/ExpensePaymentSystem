﻿using FinalCase.Base.Response;
using FinalCase.Schema.AppRoles.Responses;
using MediatR;

namespace FinalCase.Business.Features.ApplicationUsers.Queries.GetAll;
public record GetAllEmployeesQuery(bool IncludeDeleted) : IRequest<ApiResponse<IEnumerable<EmployeeResponse>>>;