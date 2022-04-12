using CSCommonSecrets;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Security.Cryptography;

/// <summary>
/// For async testing purposes only
/// </summary>
public sealed class SecurityAsyncFunctions : ISecurityAsyncFunctions
{
    private readonly Microsoft.JSInterop.IJSRuntime runtime;
    public SecurityAsyncFunctions(Microsoft.JSInterop.IJSRuntime jsRuntime)
    {
        this.runtime = jsRuntime;
    }

    /// <summary>
    /// Get AES CTR Allowed Counter Length
    /// </summary>
    /// <returns>Length</returns>
    public int AES_CTRAllowedCounterLength()
    {
        return 16;
    }

    /// <summary>
    /// AES encryption in async land
    /// </summary>
    /// <param name="bytesToEncrypt">Bytes to encrypt</param>
    /// <param name="key">Key</param>
    /// <param name="initialCounter">Initial counter</param>
    /// <returns></returns>
    public async Task<byte[]> AES_Encrypt(byte[] bytesToEncrypt, byte[] key, byte[] initialCounter)
    {
        if (bytesToEncrypt == null)
        {
            throw new NullReferenceException("bytesToEncrypt is null!");
        }
        else if (bytesToEncrypt.Length < 1)
        {
            throw new Exception("Cannot encrypt zero bytes!");
        }
        
        return await this.runtime.InvokeAsync<byte[]>("cryptAES_CTR", bytesToEncrypt, key, initialCounter);
    }

    /// <summary>
    /// Get ChaCha20 Allowed Nonce Length
    /// </summary>
    /// <returns>Length</returns>
    public int ChaCha20AllowedNonceLength()
    {
        return 12;
    }

    /// <summary>
    /// ChaCha20 encryption in async land
    /// </summary>
    /// <param name="bytesToEncrypt">Bytes to encrypt</param>
    /// <param name="key">Key</param>
    /// <param name="nonce">Nonce</param>
    /// <param name="counter">Counter</param>
    /// <returns></returns>
    public async Task<byte[]> ChaCha20_Encrypt(byte[] bytesToEncrypt, byte[] key, byte[] nonce, uint counter)
    {
        await Task.Delay(1);

        throw new NotImplementedException();

        /*byte[] returnArray = new byte[bytesToEncrypt.Length];
        using (ChaCha20 forEncrypting = new ChaCha20(key, nonce, counter))
        {
            forEncrypting.EncryptBytes(returnArray, bytesToEncrypt, bytesToEncrypt.Length);
        }

        return returnArray;*/
    }

    private static readonly Dictionary<KeyDerivationPseudoRandomFunction, string> keyDerivationPseudoRandomFunctionToJS = new Dictionary<KeyDerivationPseudoRandomFunction, string>()
    {
        { KeyDerivationPseudoRandomFunction.HMACSHA256, "SHA-256" },
        { KeyDerivationPseudoRandomFunction.HMACSHA512, "SHA-512" },
    };

    /// <summary>
    /// Performs key derivation using the PBKDF2 algorithm
    /// </summary>
    /// <param name="password">Password</param>
    /// <param name="salt">Salt</param>
    /// <param name="prf">Key derivation algorithm</param>
    /// <param name="iterationCount">Iteration count</param>
    /// <param name="numBytesRequested">Number of bytes requested</param>
    /// <returns></returns>
    public async Task<byte[]> Pbkdf2(string password, byte[] salt, KeyDerivationPseudoRandomFunction prf, int iterationCount, int numBytesRequested)
    {
        string hashFunction = keyDerivationPseudoRandomFunctionToJS[prf];
        return await this.runtime.InvokeAsync<byte[]>("deriveKeyWithPbkdf2", password, salt, iterationCount, hashFunction, numBytesRequested);
    }

    /// <summary>
    /// SHA256 hash async land
    /// </summary>
    /// <param name="bytesToHash"></param>
    /// <returns></returns>
    public async Task<byte[]> SHA256_Hash(byte[] bytesToHash)
    {
        return await this.runtime.InvokeAsync<byte[]>("hashSha256", bytesToHash);
    }

    private static System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create();

    /// <summary>
    /// Generate random bytes
    /// </summary>
    /// <param name="byteArray"></param>
    public void GenerateSecureRandomBytes(byte[] byteArray)
    {
        RandomNumberGenerator.Create().GetBytes(byteArray);
    }

    /// <summary>
    /// Generate random bytes
    /// </summary>
    /// <param name="byteArray"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    public void GenerateSecureRandomBytes(byte[] byteArray, int offset, int count)
    {
        RandomNumberGenerator.Create().GetBytes(byteArray, offset, count);
    }
}