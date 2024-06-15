﻿using Authorization.Domain;

namespace Common.Domain
{
    public class RefreshToken
    {
        public Guid RefreshTokenId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
