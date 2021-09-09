namespace Net5.Sample.API.Auth
{
    public interface IAuth
    {
        string Authenticate(string p_apiKey, string p_apiSecret);
    }
}
