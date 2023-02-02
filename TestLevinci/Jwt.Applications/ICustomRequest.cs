using Jwt.Infrastructure.Models;
using MediatR;

namespace Jwt.Applications
{
    public interface ICustomRequest<T> : IRequest<ApiResult<T>>
    {
    }

    public interface ICustomRequestHandler<TIn, TOut> : IRequestHandler<TIn, ApiResult<TOut>>
        where TIn : ICustomRequest<TOut>
    {
    }
}