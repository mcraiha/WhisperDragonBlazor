using CSCommonSecrets;

public sealed class KdfeAndPassword
{
    public readonly KeyDerivationFunctionEntry kdfe;
    public readonly string password;
    public KdfeAndPassword(KeyDerivationFunctionEntry kdfe, string password)
    {
        this.kdfe = kdfe;
        this.password = password;
    }
}