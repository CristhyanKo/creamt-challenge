﻿using CreaMT.Domain.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace CreaMT.infrastructure.Security.Cryptography;
public class Sha512Encripter : IPasswordEncripter
{
    private readonly string _additionalKey;
    public Sha512Encripter(string additionalkey) => _additionalKey = additionalkey;

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }


    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}
