using DisneyAlk.Abstractions;

namespace DisneyApiRest.Configuration
{
    public class TokenParameters : ITokenParameters
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
        public string Id { get; set; }
    }
}
