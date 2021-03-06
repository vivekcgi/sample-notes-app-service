﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace notes_service.Extensions
{
    public class VerifyHeaders
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VerifyHeaders> _logger;
        public VerifyHeaders(RequestDelegate next, ILogger<VerifyHeaders> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            string clientId = context.Request.Headers["x-auth-client-id"];
            string userId = context.Request.Headers["x-auth-user-id"];
            string userName = context.Request.Headers["x-auth-user-name"];

            if (string.IsNullOrEmpty(clientId))
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { success = false, message = "Unknown client" }));
                return;
            }

            if (string.IsNullOrEmpty(userId))
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { success = false, message = "Unknown user" }));
            }

            _logger.LogInformation($"Client Id: {clientId}");
            _logger.LogInformation($"User Id : {userId}");
            _logger.LogInformation($"User Name : {userName}");

            await _next(context);
        }
    }
}
