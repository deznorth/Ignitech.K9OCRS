﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Entities;
using DataAccess.Modules.Contracts;
using DataAccess.Clients.Contracts;
using System;
using DataAccess.Constants;
using System.Collections.Generic;
using System.IO;
using K9OCRS.Extensions;
using Serilog;
using K9OCRS.Models;
using K9OCRS.Models.ClassManagement;
using K9OCRS.Configuration;
using System.Linq;

namespace K9OCRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassSectionsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IStorageClient storageClient;
        private readonly IConnectionOwner connectionOwner;
        private readonly DbOwner dbOwner;
        private readonly ServiceConstants serviceConstants;

        public ClassSectionsController(
            ILogger logger,
            ServiceConstants serviceConstants,
            IStorageClient storageClient,
            IConnectionOwner connectionOwner,
            DbOwner dbOwner
        )
        {
            this.logger = logger;
            this.storageClient = storageClient;
            this.connectionOwner = connectionOwner;
            this.dbOwner = dbOwner;

            this.serviceConstants = serviceConstants;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassSectionResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var result = await connectionOwner.Use(async conn =>
            {
                // Get all sections
                var sections = await dbOwner.ClassSections.GetAll(conn);
                // Get all instructors associated to sections
                var instructorIds = sections.Select(s => s.InstructorID);
                var instructors = (await dbOwner.Users.GetByIDs(conn, instructorIds)).ToList();
                // Get all meetings
                var meetings = await dbOwner.ClassMeetings.GetAll(conn);

                // Group meetings by classSectionID
                var groupedMeetings = meetings.Aggregate(new Dictionary<int, List<ClassMeeting>>(), (agg, m) =>
                {
                    if (agg.ContainsKey(m.ClassSectionID))
                    {
                        agg[m.ClassSectionID].Add(m);
                    }
                    else
                    {
                        agg.Add(m.ClassSectionID, new List<ClassMeeting> { m });
                    }
                    return agg;
                });

                return sections.Select(s => new ClassSectionResult(
                    s,
                    groupedMeetings.ContainsKey(s.ID) ? groupedMeetings[s.ID] : null,
                    instructors.Find(u => u.ID == s.InstructorID)
                ));
            });

            return Ok(result);
        }

        [HttpGet("{classSectionId}")]
        [ProducesResponseType(typeof(ClassSectionDetails), 200)]
        public async Task<IActionResult> GetByID(int classSectionId)
        {
            var result = await connectionOwner.Use(async conn =>
            {
                var section = await dbOwner.ClassSections.GetByID(conn, classSectionId);
                var meetings = await dbOwner.ClassMeetings.GetByID(conn, "ClassSectionID", section.ID);
                var type = await dbOwner.ClassTypes.GetByID(conn, section.ClassTypeID);
                var instructor = await dbOwner.Users.GetByID(conn, section.InstructorID);

                return new ClassSectionDetails(section, meetings, instructor, new ClassTypeResult(type, serviceConstants.storageBasePath));
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{classSectionId}")]
        public async Task<IActionResult> Delete(int classSectionId, [FromQuery] bool hardDelete = false)
        {
            throw new NotImplementedException();
        }

        #region Class Meetings

        [HttpPost("/meetings")]
        public async Task<IActionResult> AddMeetings()
        {
            throw new NotImplementedException();
        }

        [HttpPut("/meetings")]
        public async Task<IActionResult> UpdateMeeting()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("/meetings/{classMeetingId}")]
        public async Task<IActionResult> DeleteMeeting(int classMeetingId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}