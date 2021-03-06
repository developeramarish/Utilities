﻿using System;
using System.Configuration;

namespace Structura.Shared.Utilities
{
    public interface ISettingsRetriever
    {
        T Get<T>(string name);
        string GetConnectionString(string name);
    }

    public class SettingsRetriever : ISettingsRetriever
    {
        public T Get<T>(string name)
        {
            var value = ConfigurationManager.AppSettings[name];
            Check.RequireNotNull(value, "AppSetting with name {0} not found. Please check the application configuration file.", name);
            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public string GetConnectionString(string name)
        {
            var value = ConfigurationManager.ConnectionStrings[name];
            Check.RequireNotNull(value, "ConnectionString with name {0} not found. Please check the application configuration file.", name);
            return value.ConnectionString;
        }
    }

}
