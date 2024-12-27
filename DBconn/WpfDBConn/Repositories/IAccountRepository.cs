using System.Collections.Generic;
using WpfDBConn.Models;

namespace WpfDBConn.Repositories
{
  public interface IAccountRepository
  {
    void Delete(int id);
    List<Account> GetAll();
    long Insert(Account account);
    void Update(Account account);
  }
}