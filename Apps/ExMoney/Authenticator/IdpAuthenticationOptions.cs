namespace ExMoney.Authenticator
{
    public class IdpAuthenticationOptions
    {
        public string RealmOrDomain { get; set; }
        public string ServerUrl { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string Secret { get; set; }
    }
}
