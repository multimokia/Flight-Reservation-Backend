using System;
using System.Security.Cryptography;
using System.Text;
static class Utilities
{
    /// <summary>
    /// Simple SHA1 hash function
    /// </summary>
    /// <param name="input"></param>
    /// <returns>A SHA1 hashed string from the input string</returns>
    public static string HashString(string input)
    {
        using (SHA1 sha1 = SHA1.Create())
        {
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
