using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ElProyecteGrandeSprint1.Models.Entities.DatabaseEntities;

using System;
using System.Security.Cryptography;
using System.Text;
public class Password
{
    private string _sSourceData;
    private byte[] _tmpSource;
    private byte[] _tmpHash;
    private string _sPassword;
    private MD5 _hasher;

    public Password()
    {
        _hasher = MD5.Create();
        this._sSourceData = "";
        this._tmpSource = new byte[16];
        this._tmpHash = new byte[16];
    }
    public string UserPassword
    {
        get => _sPassword;
        set
        {
            _sSourceData = value;
            _tmpSource = Encoding.ASCII.GetBytes(_sSourceData);
            _tmpHash = _hasher.ComputeHash(_tmpSource);
            _sPassword = ByteArrayToString(_tmpHash);
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
}