namespace Knapsack_Model
{
    public interface ICrypto
    {
        string Encrypt(string message);
        string Decrypt(string message);
    }
}