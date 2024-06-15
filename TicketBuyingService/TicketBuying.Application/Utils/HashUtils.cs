﻿using System.Security.Cryptography;
using System.Text;

namespace TicketBuying.Application.Utils;

public static class HashUtil
{
    private const int KeySize = 64;

    private const int Iterations = 241293;

    private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public static string GetHashGuid(string input)
    {
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(input),
            salt,
            Iterations,
            HashAlgorithm,
            KeySize);

        var saltString = Convert.ToBase64String(salt);
        var hashString = Convert.ToHexString(hash);
        var hashToDb = saltString + hashString;
        return hashToDb;
    }

    public static bool VerifyGuid(string input, string hash)
    {
        var splitHash = hash.Split("==");
        var saltString = splitHash[0] + "==";
        var hashString = splitHash[1];

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(input, Convert.FromBase64String(saltString), Iterations,
            HashAlgorithm, KeySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hashString));
    }
}
