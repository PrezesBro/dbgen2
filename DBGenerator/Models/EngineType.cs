using System.ComponentModel.DataAnnotations;

namespace DBGenerator.Models
{
    public enum EngineType
    {
        [Display(Name = "SQL Server (MSSQL)")]
        MSSQL,

        [Display(Name = "Oracle Database")]
        Oracle,

        [Display(Name = "MySQL")]
        MySQL,

        [Display(Name = "SQLite")]
        SQLite,

        [Display(Name = "PostgreeSQL")]
        Postgres,
    }
}
