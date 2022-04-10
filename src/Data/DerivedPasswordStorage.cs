using System;
using System.Collections.Generic;

public interface IReadDerivedPassword
{
	/// <summary>
	/// Does derived password exist
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <returns><c>True</c> if it exists; <c>False</c> otherwise</returns>
	bool DoesDerivedPasswordExist(string id);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <returns>Derived password</returns>
	byte[] GetDerivedPassword(string id);
}

public interface IWriteDerivedPassword
{
	/// <summary>
	/// Store a derived password
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <param name="derivedPassword">Derived password</param>
	void StorePassword(string id, byte[] derivedPassword);
}

/// <summary>
/// Class for storing (read and write) derived passwords
/// </summary>
public sealed class DerivedPasswordStorage : IReadDerivedPassword, IWriteDerivedPassword
{
	private readonly Dictionary<string, byte[]> storage = new Dictionary<string, byte[]>();
	/// <summary>
	/// Does derived password exist
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <returns><c>True</c> if it exists; <c>False</c> otherwise</returns>
	public bool DoesDerivedPasswordExist(string id)
	{
		return this.storage.ContainsKey(id);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <returns>Derived password</returns>
	public byte[] GetDerivedPassword(string id)
	{
		return this.storage[id];
	}

	/// <summary>
	/// Store a derived password
	/// </summary>
	/// <param name="id">Key ID string</param>
	/// <param name="derivedPassword">Derived password</param>
	public void StorePassword(string id, byte[] derivedPassword)
	{
		this.storage[id] = derivedPassword;
	}

	public void SecureClear()
	{
		// Overwrite with zeroes
		foreach(byte[] item in storage.Values)
		{
			Array.Clear(item, 0, item.Length);
		}

		this.storage.Clear();
	}
}