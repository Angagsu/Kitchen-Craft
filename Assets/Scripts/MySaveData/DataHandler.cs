using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System;

public class DataHandler<T> : MonoBehaviour where T : class
{
    private T data;
    private string fileName = "savedData.json";
    private string encryptionKey = "YourEncryptionKey"; // Change this to your encryption key
    private bool useEncryption = true; // Set this flag to enable/disable encryption

    private void Start()
    {
        LoadData();
    }

    public void SaveData(T dataToSave)
    {
        data = dataToSave;

        string json = JsonConvert.SerializeObject(data);
        string savedData = useEncryption ? Encrypt(json, encryptionKey) : json;

        File.WriteAllText(GetSaveFilePath(), savedData);
        Debug.Log("Data saved.");
    }

    public T LoadData()
    {
        string filePath = GetSaveFilePath();

        if (File.Exists(filePath))
        {
            string savedData = File.ReadAllText(filePath);
            string loadedData = useEncryption ? Decrypt(savedData, encryptionKey) : savedData;

            data = JsonConvert.DeserializeObject<T>(loadedData);
            Debug.Log("Data loaded.");
        }
        else
        {
            Debug.Log("No saved data found.");
        }

        return data;
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    private string Encrypt(string plainText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.Mode = CipherMode.CFB;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    private string Decrypt(string cipherText, string key)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.Mode = CipherMode.CFB;
            aesAlg.Padding = PaddingMode.PKCS7;

            byte[] iv = new byte[16];
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            Buffer.BlockCopy(cipherBytes, 0, iv, 0, iv.Length);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes, iv.Length, cipherBytes.Length - iv.Length))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}
