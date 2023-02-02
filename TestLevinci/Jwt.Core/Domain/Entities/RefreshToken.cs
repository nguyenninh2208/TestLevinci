using System;
using Jwt.Core.Shared;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Jwt.Core.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public RefreshToken(string token, DateTime expires, int userId, string remoteIpAddress)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
            RemoteIpAddress = remoteIpAddress;
        }

        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public int UserId { get; private set; }
        public bool Active => DateTime.UtcNow <= Expires;
        public string RemoteIpAddress { get; private set; }
    }
}