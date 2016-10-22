﻿using SOZA_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOZA_web
{
    public class AndroidHelper
    {
        private ApplicationDbContext _dbContext;

        public AndroidHelper(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public bool IsValidToken(string androidToken)
        {
            var androidClient = _dbContext.AndroidClients.FirstOrDefault(a => a.Token == androidToken);
            return androidClient != null && androidClient.Guardian == null;
        }

        public void RegisterUserToken(string userId, string androidToken)
        {
            if (IsValidToken(androidToken))
            {
                var androidClient = _dbContext.AndroidClients.FirstOrDefault(a => a.Token == androidToken);
                androidClient.Guardian = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
                _dbContext.SaveChanges();
            }
        }

        public string GenerateToken()
        {
            string newToken = "";
            while (_dbContext.AndroidClients.Any(a => a.Token == newToken) || newToken == "")
                newToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);

            _dbContext.AndroidClients.Add(new AndroidClient()
            {
                Token = newToken
            });
            _dbContext.SaveChanges();

            return newToken;
        }
    }
}