﻿using DataAccess.Constants;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;


namespace DataAccess.Repositories
{
    public class DogsRepository : BaseRepository<Dog>
    {

        // Everytime that you create a repository, make sure you include a constructor that calls the "base constructor"
        // passing in the Db table name that is associated to it by using this syntax
        public DogsRepository(IHttpContextAccessor _httpContextAccessor) : base(DbTables.Get(nameof(Dog)), _httpContextAccessor) { }
    }
}
