﻿using CityManagerAPİ.Models;
using Microsoft.EntityFrameworkCore;

namespace CityManagerAPİ.Data;

public class AuthRepository : IAuthRepository
{
	private readonly DataContext _context;

	public AuthRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<User> LoginAsync(string username, string password)
	{
		var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

		if (user == null)
		{
			return null!;
		}

		if (!VarifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
		{
			return null!;
		}
		return user;
	}

	private bool VarifyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt)
	{
		using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
		{
			var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

			for (int i = 0; i < computedHash.Length; i++)
			{
				if (passwordHash[i] != computedHash[i])
				{
					return false;
				}
			}
			return true;
		}
	}

	public async Task<User> RegisterAsync(User user, string password)
	{
		byte[] passwordHash, passwordSalt;
		CreatePasswordHash(password, out passwordHash, out passwordSalt);
		user.PasswordHash = passwordHash;
		user.PasswordSalt = passwordSalt;

		await _context.Users.AddAsync(user);
		await _context.SaveChangesAsync();
		return user;
	}

	private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
	{
		using (var hmac = new System.Security.Cryptography.HMACSHA512())
		{
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}
	}

	public async Task<bool> UserExists(string username)
	{
		if (await _context.Users.AnyAsync(u => u.Username == username))
		{
			return true;
		}
		return false;
	}
}
