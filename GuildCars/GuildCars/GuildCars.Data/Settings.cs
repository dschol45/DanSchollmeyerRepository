﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GuildCars.Data
{
    public class Settings
    {
        private static string _connectionString;
        private static string _repositoryType;

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return _connectionString;
        }

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_repositoryType))
                _repositoryType = ConfigurationManager.AppSettings["Mode"].ToString();

            return _repositoryType;
        }
    }
}
