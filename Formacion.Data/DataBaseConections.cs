using Microsoft.EntityFrameworkCore;

namespace Formacion.Data
{

    /// <summary>
    /// class to have centralized connection strings in a normal application this information should go in a configuration file 
    /// </summary>
    public class DataBaseConections<TContext> where TContext : DbContext
    {
        public static DbContextOptions<TContext> ContextOptions { get; private set; }
        private const string connectionSqlServer = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Formacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private static TypeConnections typeConnection = TypeConnections.SQLServer;

        public static void InitializeContext(DbContextOptions<TContext> context)
        {
           if(context == null)
           {
                ContextOptions = new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionSqlServer)
                .Options;
                return;
            }
           ContextOptions = context; 
        }
 
        private static DbContextOptions<TContext> GetDbConextOptionsSQLServer()
        {
            return new DbContextOptionsBuilder<TContext>()
                .UseSqlServer(connectionSqlServer)
                .Options;
        }
    }
    public enum TypeConnections
    {
        SQLServer,
        InMemory
    }

}
