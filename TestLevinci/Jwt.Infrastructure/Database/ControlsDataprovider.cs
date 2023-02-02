using Jwt.Infrastructure.Configuations;
using System;
using System.Data;
using System.Reflection;

namespace Jwt.Infrastructure.Database
{
    public static class ControlsDataprovider
    {
        static ControlsDataprovider()
        {
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class ControlsDataproviderAttribute : Attribute
        {
            public ControlsDataproviderAttribute(string db, string name, CommandType type)
            {
                this.Database = db;
                this.Name = name;
                this.Type = type;
            }
            public string Database { get; private set; }
            public string Name { get; private set; }
            public CommandType Type { get; private set; }
        }

        public static class StoreAndFunctionInfo
        {
            /// <summary>
            /// SP_Mana_UserLogin
            /// </summary>
            [ControlsDataprovider(nameof(Utility.Setting.ConnectionString), "TestLevinci..SP_Mana_UserLogin", CommandType.StoredProcedure)]
            public static string SP_Mana_UserLogin { get => "SP_Mana_UserLogin"; }

            /// <summary>
            /// SP_Mana_UserUpdate
            /// </summary>
            [ControlsDataprovider(nameof(Utility.Setting.ConnectionString), "TestLevinci..SP_Mana_UserUpdate", CommandType.StoredProcedure)]
            public static string SP_Mana_UserUpdate { get => "SP_Mana_UserUpdate"; }
            /// <summary>
            /// SP_Mana_UserUpdate
            /// </summary>
            [ControlsDataprovider(nameof(Utility.Setting.ConnectionString), "TestLevinci..SP_Mana_UserDelete", CommandType.StoredProcedure)]
            public static string SP_Mana_UserDelete { get => "SP_Mana_UserDelete"; }

         
        }

        /// <summary>
        /// get value attribute
        /// </summary>
        /// <param name="propertyName">property cần lấy</param>
        /// <returns></returns>
        public static ControlsDataproviderAttribute GetAttribute(string propertyName)
        {
            PropertyInfo propertyInfo = typeof(StoreAndFunctionInfo).GetProperty(propertyName.ToString());
            ControlsDataproviderAttribute attribute = (ControlsDataproviderAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ControlsDataproviderAttribute));
            if (attribute != null)
            {
                return attribute;
            }
            return null;
        }
    }
}
