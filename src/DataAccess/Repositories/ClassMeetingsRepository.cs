﻿using DataAccess.Constants;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class ClassMeetingsRepository : BaseRepository<ClassMeeting>
    {
        // Everytime that you create a repository, make sure you include a constructor that calls the "base constructor"
        // passing in the Db table name that is associated to it by using this syntax
        public ClassMeetingsRepository() : base(DbTables.Get(nameof(ClassMeeting))) { }
    }
}