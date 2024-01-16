using FreeSql;
using Microsoft.Data.SqlClient;

namespace FreeSqlExample.Basic
{
    public class BasicOrm
    {
        private const string ConnectionString =
               $@"Data Source=49.234.11.101;Database={DatabaseName};User ID=sa;Password=yf1987416;";

        /// <summary>
        /// 连接字符串如果不指定数据库名称，可能会在master数据库中创建表
        /// </summary>
        public const string DatabaseName = "FreeSqlDatabase";

        public static IFreeSql Orm { get; private set; }

        static BasicOrm()
        {
            CreateDatabase();
            Orm = new FreeSqlBuilder()
                      .UseConnectionString(DataType.SqlServer, ConnectionString, null)
                      .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                      .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                      .Build();
        }

        /// <summary>
        /// 不建议在代码中创建数据库
        /// </summary>
        private static void CreateDatabase()
        {
            // 设置连接字符串，连接到 SQL Server 实例的默认数据库（通常是 master）
            string connectionString = "Server=49.234.11.101;Integrated Security=True;";
            string dbName = DatabaseName;
            // 定义检查数据库是否存在的 SQL 命令
            string checkDatabaseExistsSql = $"SELECT database_id FROM sys.databases WHERE name = @dbName;";
            // 定义创建数据库的 SQL 命令
            string createDatabaseSql = $"CREATE DATABASE [{dbName}];";
            // 创建连接
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // 创建命令
                using (SqlCommand command = new SqlCommand(checkDatabaseExistsSql, connection))
                {
                    // 添加参数以防止 SQL 注入
                    command.Parameters.AddWithValue("@dbName", dbName);
                    // 打开连接
                    connection.Open();
                    // 执行查询并获取结果
                    object result = command.ExecuteScalar();
                    // 如果返回值为 null，则数据库不存在，需要创建
                    if (result == null)
                    {
                        // 更改命令文本以创建数据库
                        command.CommandText = createDatabaseSql;
                        // 移除不必要的参数
                        command.Parameters.Clear();
                        // 执行创建数据库的命令
                        command.ExecuteNonQuery();
                        Console.WriteLine($"数据库 '{dbName}' 已创建。");
                    }
                    else
                    {
                        Console.WriteLine($"数据库 '{dbName}' 已存在。");
                    }
                }
            }
        }
    }
}