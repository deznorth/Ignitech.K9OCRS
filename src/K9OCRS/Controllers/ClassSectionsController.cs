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
using System.Data.SqlClient;

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
                var sections = await dbOwner.ClassSections.GetAll(conn);
                return sections.Select(s => s.ToClassSectionResult(serviceConstants.storageBasePath));
            });

            return Ok(result);
        }

        [HttpGet("{classSectionId}")]
        [ProducesResponseType(typeof(ClassSectionResult), 200)]
        public async Task<IActionResult> GetByID(int classSectionId)
        {
            var result = await connectionOwner.Use(async conn =>
            {
                var section = await dbOwner.ClassSections.GetByID(conn, classSectionId);
                return section.ToClassSectionResult(serviceConstants.storageBasePath);
            });

            return Ok(result);
        }

        [HttpGet("roster/{classSectionId}")]
        //[ProducesResponseType(typeof(ClassSectionResult), 200)]
        public async Task<IActionResult> GetRoster(int classSectionId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        public async Task<IActionResult> Add(ClassSectionAddRequest request)
        {
            var result = await connectionOwner.UseTransaction(async (conn, tr) =>
            {
                var section = await dbOwner.ClassSections.Add(conn, tr, new ClassSection
                {
                    ClassTypeID = request.ClassTypeID,
                    InstructorID = request.InstructorID,
                    RosterCapacity = request.RosterCapacity,
                });

                // Update meetings to have the correct section id
                var assignedMeetings = request.Meetings.Select(m =>
                {
                    m.ClassSectionID = section.ID;
                    return m;
                }).ToList();

                var meetings = await dbOwner.ClassMeetings.AddMany(conn, tr, assignedMeetings);

                section.Meetings = meetings.ToList();

                tr.Commit();

                return section;
            });

            return Ok(result.ID);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClassSectionUpdateRequest request)
        {
            try
            {
                await connectionOwner.UseTransaction(async (conn, tr) =>
                {
                    var updatedCount = await dbOwner.ClassSections.Update(conn, tr, new ClassSection
                    {
                        ID = request.ID,
                        ClassTypeID = request.ClassTypeID,
                        InstructorID = request.InstructorID,
                        RosterCapacity = request.RosterCapacity,
                    });

                    if (updatedCount < 1) throw new KeyNotFoundException();

                    int deletedCount = 0;
                    int insertedCount = 0;

                    if (request.MeetingIdsToDelete.Count() > 0)
                    {
                        deletedCount = await dbOwner.ClassMeetings.DeleteMany(conn, tr, request.MeetingIdsToDelete);
                    }

                    if (request.MeetingsToInsert.Count() > 0)
                    {
                        // Update meetings to have the correct section id
                        var assignedMeetings = request.MeetingsToInsert.Select(m =>
                        {
                            m.ClassSectionID = request.ID;
                            return m;
                        }).ToList();

                        insertedCount = (await dbOwner.ClassMeetings.AddMany(conn, tr, assignedMeetings)).Count();
                    }

                    if (
                        request.MeetingIdsToDelete.Count() != deletedCount ||
                        request.MeetingsToInsert.Count() != insertedCount
                    )
                    {
                        throw new Exception();
                    }

                    tr.Commit();
                });

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                {
                    return NotFound();
                }

                logger.Error(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{classSectionId}")]
        public async Task<IActionResult> Delete(int classSectionId)
        {
            // Prevent deletion of placeholder section
            if (classSectionId <= 1) return BadRequest("ID must be greater than 1");
            try
            {
                await connectionOwner.UseTransaction(async (conn, tr) =>
                {
                    var deletedCount = await dbOwner.ClassSections.Delete(conn, tr, classSectionId);

                    if (deletedCount < 1) throw new KeyNotFoundException();

                    tr.Commit();
                });

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException)
                {
                    return NotFound();
                }

                logger.Error(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}
