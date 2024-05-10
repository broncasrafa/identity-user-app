namespace Web.Api.Core.Application.Settings;

public class JWTSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double ExpiresInHours{ get; set; }
    public double ExpiresInMinutes { get; set; }
    public double ExpiresInDays { get; set; }
}
