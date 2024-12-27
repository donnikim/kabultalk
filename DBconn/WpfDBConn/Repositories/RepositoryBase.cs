using CommonLib.Database;

namespace WpfDBConn.Repositories
{
  public abstract class RepositoryBase
  {
    public MySqlDb GetKabulTalkDb()
    {
      return new MySqlDb(Properties.Settings.Default.KABULTALK_DB_CONN_STR);
    }
  }
}
