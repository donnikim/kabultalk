using System.Collections.Generic;
using System.Windows.Input;
using WpfDBConn.Commands;
using WpfDBConn.Models;
using WpfDBConn.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Security.Cryptography;
using System.Data;


namespace WpfDBConn.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
        //private IAccountRepository _accountRepository;
        //private List<Account> _accounts = default!;

        private MySqlConnection _conn = default;
    
    private void Connection(object _)
    {
        string connnectionString = "UID=DB계정;PWD=비밀번호;Server=127.0.0.1;Port=3306;Database=kabultolk";
       
        try
        {
          _conn = new MySqlConnection(connnectionString);
          _conn.Open();
        }
        catch (Exception ex)
        {
                MessageBox.Show($"Connction fail \n({ex.Message}");
        }
     }

    private void Insert(object _)
    {
        string query = "insert into account(\n" +
                 "email,pwd,nickname,cell_phone\n" +
                 ") values (\n" +
                 "@email,@pwd, @nickname,@cell_phone\n" +
                 ");";
        using MySqlCommand cmd = _conn.CreateCommand();
        cmd.CommandText = query;
        cmd.Parameters.Add("@email", MySqlDbType.VarChar);
        cmd.Parameters.Add("@pwd", MySqlDbType.Text);
        cmd.Parameters.Add("@nickname", MySqlDbType.VarChar);
        cmd.Parameters.Add("@cell_phone", MySqlDbType.VarChar);

        cmd.Parameters["@email"].Value = "test3@test.com";
        cmd.Parameters["@pwd"].Value = "1q2w3e4r";
        cmd.Parameters["@nickname"].Value = "myNick";
        cmd.Parameters["@cell_phone"].Value = "01011112222";

        cmd.ExecuteNonQuery();
        long id = cmd.LastInsertedId;
        MessageBox.Show(id.ToString());
     }

    private void Update(object _)
    {
            string query =
        "UPDATE account SET " +
        "email = @email, " +
        "pwd = @pwd, " +
        "nickname = @nickname, " +
        "cell_phone = @cell_phone " +
        "WHERE id = @id";

            // Ensure the connection is open
            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
            }

            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                // Set the CommandText
                cmd.CommandText = query;

                // Add parameters
                cmd.Parameters.AddWithValue("@email", "qqqq@kabul.com");
                cmd.Parameters.AddWithValue("@pwd", "3214");
                cmd.Parameters.AddWithValue("@nickname", "qwer");
                cmd.Parameters.AddWithValue("@cell_phone", "0102345678");
                cmd.Parameters.AddWithValue("@id", 3);

                // Execute the command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

        }

    private void Delete(object _)
    {
        string query = "" +
        "delete From account where id = @id;";
            using (MySqlCommand cmd = _conn.CreateCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@id", 3);
                cmd.ExecuteNonQuery();
            }
    }

        private void Select(object _)
        {
            /*
              연결형 (DataReader)
              - 한줄씩 읽어가며 데이터를 불러온다
              - 장점 : 속도가 매우 빠르다
              - 단점 : 끝이 언제 도달할지 모두 읽어봐야 알 수 있다.
            */
            /*
           string query = "select * from account;";
           using (MySqlCommand cmd = _conn.CreateCommand())
           {
                cmd.CommandText= query;
                //cmd.Parameters.AddWithValue("@id", 1);
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) {

                      var email = (string)dr["email"];
                      var pwd = (string)dr["pwd"];
                      var nickname = (string)dr["nickname"];
                      var cell_phone = (string)dr["cell_phone"];

                            MessageBox.Show($"email:{email} / pwd:{pwd} / nickname: {nickname} / cell_phone: {cell_phone}");

                        ----start
                        data1 -> true
                        data2 -> true
                        data3 -> true
                        ---end -> false

                    }
                };
           };
           */
            /*
              비연결형 (DataTable)
              - 데이터 스키마 정보를 모두 테이블에 저장한다.
              - 장점 : 스키마 정보가 모두 존재하므로 데이터를 가공하고 총 갯수를 체크하기 용이 하다.
              - 단점 : 속도가 상대적으로 느리고 메모리를 많이 차지한다.
            */
            DataTable dt = new();
            string query = "select * from account";
            using (MySqlCommand cmd = _conn.CreateCommand())
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandText = query;
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string? email = (string)dr["email"];
                    string? pwd = (string)dr["pwd"];
                    string? nickname = (string)dr["nickname"];
                    string? cell_phone = (string)dr["cell_phone"];

                    MessageBox.Show($"email : {email} / pwq : {pwd} / nickname : {nickname} / cell_phone = {cell_phone}");
                }
            }
        }

    public MainViewModel(IAccountRepository accountRepository)
    {

    }

   
    public ICommand ConnectionCommand => new RelayCommand<object>(Connection);
    public ICommand InsertCommand => new RelayCommand<object>(Insert);
    public ICommand UpdateCommand => new RelayCommand<object>(Update);
    public ICommand DeleteCommand => new RelayCommand<object>(Delete);
    public ICommand SelectCommand => new RelayCommand<object>(Select);
  }
}
