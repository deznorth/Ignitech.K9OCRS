﻿using Dapper;
using DataAccess.Constants;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        // Everytime that you create a repository, make sure you include a constructor that calls the "base constructor"
        // passing in the Db table name that is associated to it by using this syntax
        public UsersRepository() : base(DbTables.Get(nameof(User))) { }

        public async Task<string> GetByEmail(IDbConnection conn, string email)
        {
            var query = $"SELECT Email FROM {_tableName} WHERE Email = @Email";
            string result = conn.QueryFirstOrDefault<string>(query, new { Email = email });
            return result;
        }

        public async Task<IEnumerable<User>> GetIdByLogin(IDbConnection conn, string email, string password)
        {
            var query = $"SELECT ID, UserRoleID, FirstName, LastName FROM {_tableName} WHERE Email = @Email AND Password = @Password";
            return await conn.QueryAsync<User>(query, new { Email = email, Password = password});
        }
    }
}
