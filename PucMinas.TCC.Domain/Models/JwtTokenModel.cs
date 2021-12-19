using System;

namespace PucMinas.TCC.Domain.Models
{
    public class JwtTokenModel
    {
        public bool Authenticated { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Expiration { get; set; }
        public string AccessToken { get; set; }

        public bool IsValid()
        {
            if (Expiration.HasValue && Expiration.Value > DateTime.UtcNow)
                return true;

            return false;
        }
    }
}
