using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestStarcounterServer
{
    public static class CRUDsHelper
    {

        public static TProxy ToProxy<TProxy, TDatabase>(TDatabase row)
            where TProxy : class, new()
        {
            TProxy proxy = new TProxy();
            Type proxyType = typeof(TProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            // only take if proxyProperty exists in databaseProperties 
            // proxy can be subset of database
            // proxy can have own properties
            // database can have computed/ReadOnly properties, copy also
            foreach (PropertyInfo proxyProperty in proxyProperties)
            {
                var dbP = row.GetType().GetProperty(proxyProperty.Name); //?.GetValue(row);

                if (dbP != null)
                {
                    object value = dbP.GetValue(row);

                    if (value != null && dbP.PropertyType.GetTypeInfo().IsClass && dbP.PropertyType != typeof(string))
                        proxyProperty.SetValue(proxy, value.GetObjectNo()); //v.GetObjectNo());  //Db.FromId(2));
                    else
                    {
                        value = ConvertToProxyValue(dbP.PropertyType, value);
                        proxyProperty.SetValue(proxy, value);
                    }
                }
            }
            // Should every proxy hase Key property? Maybe not
            proxy.GetType().GetProperty("RowKey")?.SetValue(proxy, row.GetObjectNo());

            return proxy;
        }

        public static TDatabase deneme<TDatabase>(ulong? v)
            where TDatabase : class, new()
        {
            TDatabase row = null;
            Type databaseType = typeof(TDatabase);
            PropertyInfo[] databaseProperties = databaseType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            PropertyInfo dbProperty = databaseProperties.FirstOrDefault(x => x.Name == "P");    // ObjectReference AHT P
            Type aaa = dbProperty.GetType();
            //typeof(dbProperty.GetType());

            //object result = Convert.ChangeType((decimal)2.12345, typeof(double));//dbProperty.GetType());

            row = Db.FromId<TDatabase>(4);

            if (v != null && dbProperty.PropertyType.GetTypeInfo().IsClass && dbProperty.PropertyType != typeof(string))
                dbProperty.SetValue(row, Db.FromId((ulong)v)); //v.GetObjectNo());  //Db.FromId(2));
            else
                dbProperty.SetValue(row, v);
            /*
            if (dbProperty.PropertyType.IsSerializable)
                dbProperty.SetValue(row, v);
            else
                dbProperty.SetValue(row, Db.FromId(v));// v.GetObjectNo());  //Db.FromId(2));
            */
            return row;
        }

        public static TDatabase FromProxy<TProxy, TDatabase>(TProxy proxy)
            where TDatabase : class, new()
        {
            TDatabase row = null;
            Type proxyType = typeof(TProxy);
            Type databaseType = typeof(TDatabase);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();
            PropertyInfo[] databaseProperties = databaseType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            ulong pk = (ulong)proxy.GetType().GetProperty("RowKey")?.GetValue(proxy);

            if (pk > 0)
                row = Db.FromId<TDatabase>(pk);
            else
                row = new TDatabase();

            foreach (PropertyInfo databaseProperty in databaseProperties)
            {
                PropertyInfo proxyProperty = proxyProperties.FirstOrDefault(x => x.Name == databaseProperty.Name);

                if (proxyProperty != null)
                {
                    object value = proxyProperty.GetValue(proxy);

                    if (value != null && databaseProperty.PropertyType.GetTypeInfo().IsClass && databaseProperty.PropertyType != typeof(string))
                        databaseProperty.SetValue(row, Db.FromId((ulong)value)); //v.GetObjectNo());  //Db.FromId(2));
                    else
                    {
                        value = ConvertToDatabaseValue(databaseProperty.PropertyType, value);
                        databaseProperty.SetValue(row, value);
                    }
                }
            }

            return row;
        }

        public static object ConvertToProxyValue(Type databaseType, object value)
        {
            if (value == null)
            {
                return value;
            }

            if (databaseType == typeof(decimal))
            {
                return Convert.ToDouble(value);
            }
            else if (databaseType == typeof(decimal?))
            {
                decimal? v = value as decimal?;
                return (double?)Convert.ToDouble(v.Value);
            }
            else if (databaseType == typeof(DateTime))
            {
                DateTime v = (DateTime)value;
                return v.Ticks;
            }
            else if (databaseType == typeof(DateTime?))
            {
                DateTime? v = value as DateTime?;
                return (long?)(v.Value.Ticks);
            }

            return value;
        }

        public static object ConvertToDatabaseValue(Type databaseType, object value)
        {
            if (value == null)
            {
                return value;
            }

            if (databaseType == typeof(decimal))
            {
                return Convert.ToDecimal(value);
            }
            else if (databaseType == typeof(decimal?))
            {
                double? v = value as double?;
                return (decimal?)Convert.ToDecimal(v.Value);
            }
            else if (databaseType == typeof(DateTime))
            {
                long v = (long)value;
                return new DateTime(v);
            }
            else if (databaseType == typeof(DateTime?))
            {
                long? v = value as long?;
                return (DateTime?)(new DateTime(v.Value));
            }

            return value;
        }

    }

}
