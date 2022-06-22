using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;

using System;
using System.Security.Cryptography;
using System.Text;

public class Password
{
    private string _sPassword;
    private readonly MD5 _hasher;

    public Password()
    {
        _hasher = MD5.Create();
    }

    public Password(string sPassword) : this()
    {
        _sPassword = sPassword;
    }

    public string UserPassword
    {
        get => _sPassword;
        set
        {
            var sSourceData = value;
            var tmpSource = Encoding.ASCII.GetBytes(sSourceData);
            var tmpHash = _hasher.ComputeHash(tmpSource);
            _sPassword = ByteArrayToString(tmpHash);
        }
    }

    private string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length; i++)
        {
            sOutput.Append(arrInput[i].ToString("X2"));
        }

        return sOutput.ToString();
    }

    public bool ValidatePassword(string NewPassword)
    {

        var newtmpSource = Encoding.ASCII.GetBytes(NewPassword);
        var tmpNewHash = _hasher.ComputeHash(newtmpSource);
        var newsPassword = ByteArrayToString(tmpNewHash);
        bool bEqual = false;
        return newsPassword.Equals(_sPassword);
    }
}