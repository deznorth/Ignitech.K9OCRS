﻿using DataAccess.Entities;
using DataAccess.Repositories;
using DataAccess.Repositories.Contracts;

namespace DataAccess
{
    /// <summary>
    /// This acts as a simple collection of repository instances so you can easily access any repository you need
    /// by simply getting this class injected where you need it.
    /// </summary>
    public class DbOwner
    {
        public readonly IRepository<UserRole> UserRoles;
        public readonly IRepository<User> Users;
        public readonly IRepository<Dog> Dogs;
        public readonly IRepository<VaccinationRecord> VaccinationRecords;
        public readonly IRepository<ClassPhoto> ClassPhotos;
        public readonly IRepository<ClassType> ClassTypes;
        public readonly IRepository<ClassSection> ClassSections;
        public readonly IRepository<ClassMeeting> ClassMeetings;
        public readonly IRepository<SectionApplication> SectionApplications;

        public DbOwner()
        {
            UserRoles = new UserRolesRepository();
            Users = new UsersRepository();
            Dogs = new DogsRepository();
            VaccinationRecords = new VaccinationRecordsRepository();
            ClassPhotos = new ClassPhotosRepository();
            ClassTypes = new ClassTypesRepository();
            ClassSections = new ClassSectionsRepository();
            ClassMeetings = new ClassMeetingsRepository();
            SectionApplications = new SectionApplicationsRepository();
        }
    }
}