﻿using Microsoft.Extensions.Configuration;
using ZulAssetsBackEnd_API.BAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ZulAssetsBackEnd_API.BAL.RequestParameters;

namespace ZulAssetsBackEnd_API
{
    public static class ConfigSettings
    {

        public static string conStr1 { get; }
        //public static string conStr2 { get; }

        static ConfigSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            conStr1 = configurationBuilder.Build().GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public static string ConfigSettings_id(int i)
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            string con = "";
            switch (i)
            {
                case 1:
                    con = configurationBuilder.Build().GetSection("ConnectionStrings:DefaultConnection").Value;
                    break;
                case 2:
                    con = configurationBuilder.Build().GetSection("ConnectionStrings:DefaultConnectionKSA").Value;
                    break;
                case 3:
                    con = configurationBuilder.Build().GetSection("ConnectionStrings:DefaultConnection2").Value;
                    break;
                case 4:
                    con = configurationBuilder.Build().GetSection("ConnectionStrings:ZultimeConnectionString").Value;
                    break;
                default:
                    break;
            }
            return con;

        }

    }
}
