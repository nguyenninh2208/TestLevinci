using System.Collections.Generic;

namespace Jwt.Core.Domain.Entities.Responses
{
    public abstract class BaseGatewayResponse
    {
        protected BaseGatewayResponse(bool success = false, IEnumerable<Error> errors = null)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; }
        public IEnumerable<Error> Errors { get; }
    }
}