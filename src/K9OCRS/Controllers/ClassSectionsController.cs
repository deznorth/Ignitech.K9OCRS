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

        /// <summary>
        /// Fetch a list of class sections grouped by their class type
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassTypeResult>), 200)]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{classSectionId}")]
        public async Task<IActionResult> GetByID(int classSectionId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("type/{classTypeId}")]
        public async Task<IActionResult> GetByClassTypeID(int classTypeId)
        {
            throw new NotImplementedException();
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