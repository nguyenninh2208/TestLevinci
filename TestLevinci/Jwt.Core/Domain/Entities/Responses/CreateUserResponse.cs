using System.Collections.Generic;

namespace Jwt.Core.Domain.Entities.Responses
{
    public sealed class CreateUserResponse : BaseGatewayResponse
    {
        public CreateUserResponse(string id, bool success = false, IEnumerable<Error> errors = null) : base(success,
            errors)
        {
            Id = id;
        }

        public string Id { get; }
    }
}