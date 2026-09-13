using System;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

internal static class CustomTypes
{
	[CompilerGenerated]
	private static SerializeMethod _003C_003Ef__mg_0024cache0;

	[CompilerGenerated]
	private static DeserializeMethod _003C_003Ef__mg_0024cache1;

	[CompilerGenerated]
	private static SerializeMethod _003C_003Ef__mg_0024cache2;

	[CompilerGenerated]
	private static DeserializeMethod _003C_003Ef__mg_0024cache3;

	[CompilerGenerated]
	private static SerializeMethod _003C_003Ef__mg_0024cache4;

	[CompilerGenerated]
	private static DeserializeMethod _003C_003Ef__mg_0024cache5;

	[CompilerGenerated]
	private static SerializeMethod _003C_003Ef__mg_0024cache6;

	[CompilerGenerated]
	private static DeserializeMethod _003C_003Ef__mg_0024cache7;

	internal static void Register()
	{
		Type typeFromHandle = typeof(Vector2);
		if (_003C_003Ef__mg_0024cache0 == null)
		{
			_003C_003Ef__mg_0024cache0 = SerializeVector2;
		}
		SerializeMethod serializeMethod = _003C_003Ef__mg_0024cache0;
		if (_003C_003Ef__mg_0024cache1 == null)
		{
			_003C_003Ef__mg_0024cache1 = DeserializeVector2;
		}
		PhotonPeer.RegisterType(typeFromHandle, 87, serializeMethod, _003C_003Ef__mg_0024cache1);
		Type typeFromHandle2 = typeof(Vector3);
		if (_003C_003Ef__mg_0024cache2 == null)
		{
			_003C_003Ef__mg_0024cache2 = SerializeVector3;
		}
		SerializeMethod serializeMethod2 = _003C_003Ef__mg_0024cache2;
		if (_003C_003Ef__mg_0024cache3 == null)
		{
			_003C_003Ef__mg_0024cache3 = DeserializeVector3;
		}
		PhotonPeer.RegisterType(typeFromHandle2, 86, serializeMethod2, _003C_003Ef__mg_0024cache3);
		Type typeFromHandle3 = typeof(Quaternion);
		if (_003C_003Ef__mg_0024cache4 == null)
		{
			_003C_003Ef__mg_0024cache4 = SerializeQuaternion;
		}
		SerializeMethod serializeMethod3 = _003C_003Ef__mg_0024cache4;
		if (_003C_003Ef__mg_0024cache5 == null)
		{
			_003C_003Ef__mg_0024cache5 = DeserializeQuaternion;
		}
		PhotonPeer.RegisterType(typeFromHandle3, 81, serializeMethod3, _003C_003Ef__mg_0024cache5);
		Type typeFromHandle4 = typeof(PhotonPlayer);
		if (_003C_003Ef__mg_0024cache6 == null)
		{
			_003C_003Ef__mg_0024cache6 = SerializePhotonPlayer;
		}
		SerializeMethod serializeMethod4 = _003C_003Ef__mg_0024cache6;
		if (_003C_003Ef__mg_0024cache7 == null)
		{
			_003C_003Ef__mg_0024cache7 = DeserializePhotonPlayer;
		}
		PhotonPeer.RegisterType(typeFromHandle4, 80, serializeMethod4, _003C_003Ef__mg_0024cache7);
	}

	private static byte[] SerializeVector3(object customobject)
	{
		Vector3 vector = (Vector3)customobject;
		int targetOffset = 0;
		byte[] array = new byte[12];
		Protocol.Serialize(vector.x, array, ref targetOffset);
		Protocol.Serialize(vector.y, array, ref targetOffset);
		Protocol.Serialize(vector.z, array, ref targetOffset);
		return array;
	}

	private static object DeserializeVector3(byte[] bytes)
	{
		Vector3 vector = default(Vector3);
		int offset = 0;
		Protocol.Deserialize(out vector.x, bytes, ref offset);
		Protocol.Deserialize(out vector.y, bytes, ref offset);
		Protocol.Deserialize(out vector.z, bytes, ref offset);
		return vector;
	}

	private static byte[] SerializeVector2(object customobject)
	{
		Vector2 vector = (Vector2)customobject;
		byte[] array = new byte[8];
		int targetOffset = 0;
		Protocol.Serialize(vector.x, array, ref targetOffset);
		Protocol.Serialize(vector.y, array, ref targetOffset);
		return array;
	}

	private static object DeserializeVector2(byte[] bytes)
	{
		Vector2 vector = default(Vector2);
		int offset = 0;
		Protocol.Deserialize(out vector.x, bytes, ref offset);
		Protocol.Deserialize(out vector.y, bytes, ref offset);
		return vector;
	}

	private static byte[] SerializeQuaternion(object obj)
	{
		Quaternion quaternion = (Quaternion)obj;
		byte[] array = new byte[16];
		int targetOffset = 0;
		Protocol.Serialize(quaternion.w, array, ref targetOffset);
		Protocol.Serialize(quaternion.x, array, ref targetOffset);
		Protocol.Serialize(quaternion.y, array, ref targetOffset);
		Protocol.Serialize(quaternion.z, array, ref targetOffset);
		return array;
	}

	private static object DeserializeQuaternion(byte[] bytes)
	{
		Quaternion quaternion = default(Quaternion);
		int offset = 0;
		Protocol.Deserialize(out quaternion.w, bytes, ref offset);
		Protocol.Deserialize(out quaternion.x, bytes, ref offset);
		Protocol.Deserialize(out quaternion.y, bytes, ref offset);
		Protocol.Deserialize(out quaternion.z, bytes, ref offset);
		return quaternion;
	}

	private static byte[] SerializePhotonPlayer(object customobject)
	{
		int iD = ((PhotonPlayer)customobject).ID;
		byte[] array = new byte[4];
		int targetOffset = 0;
		Protocol.Serialize(iD, array, ref targetOffset);
		return array;
	}

	private static object DeserializePhotonPlayer(byte[] bytes)
	{
		int offset = 0;
		int value;
		Protocol.Deserialize(out value, bytes, ref offset);
		if (PhotonNetwork.networkingPeer.mActors.ContainsKey(value))
		{
			return PhotonNetwork.networkingPeer.mActors[value];
		}
		return null;
	}
}
