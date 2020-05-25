using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Caching;
using CachingSolutionsSamples.Infrastructure.Abstractions;

namespace CachingSolutionsSamples.Infrastructure.Categories
{
    internal class PolicyCreator : IPolicyCreator
    {
        private SqlChangeMonitor _monitor;
        private SqlDependency _dependency;
        private bool _hasDataChanged;
        private readonly string _query;

        public PolicyCreator(string query)
        {
            _query = query;
        }

        public CacheItemPolicy Create()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
            SqlDependency.Start(connectionString);

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            using DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = _query;
            cmd.CommandType = CommandType.Text;

            _dependency = new SqlDependency((SqlCommand) cmd);
            _dependency.OnChange += dependency_OnChange;

            using IDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
            }

            // Create a new monitor.
            _monitor = new SqlChangeMonitor(_dependency);

            // Create a policy.
            var policy = new CacheItemPolicy();
            policy.ChangeMonitors.Add(_monitor);
            policy.UpdateCallback = CacheUpdateCallback;

            // Reset the data changed flag.
            _hasDataChanged = false;

            return policy;
        }

        private void CacheUpdateCallback(CacheEntryUpdateArguments arguments)
        {
            _monitor?.Dispose();

            // Disconnect event to prevent recursion.
            _dependency.OnChange -= dependency_OnChange;

            // Refresh the cache if tracking data changes.
            if (_hasDataChanged)
            {
                // Refresh the cache item.
                var policy = Create();
                arguments.UpdatedCacheItemPolicy = policy;
            }
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            _hasDataChanged = true;
        }
    }
}