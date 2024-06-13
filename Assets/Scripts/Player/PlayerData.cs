using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;


public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
    public ulong ClientID;
    public int ColorID;
    public int PlayerTeam;
    public FixedString64Bytes PlayerName;
    public FixedString64Bytes PlayerID;

    public bool Equals(PlayerData other)
    {
        return ClientID == other.ClientID &&
            ColorID == other.ColorID &&
            PlayerTeam == other.PlayerTeam &&
            PlayerName == other.PlayerName &&
            PlayerID == other.PlayerID;
            
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientID);
        serializer.SerializeValue(ref ColorID);
        serializer.SerializeValue(ref PlayerTeam);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref PlayerID);  
    }
}
