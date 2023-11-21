using Npgsql;

string connectionString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=q1w2e3r4Z;Database=test;";
//string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=q1w2e3r4Z;Database=test;";
var con = new NpgsqlConnection(connectionString);
con.Open();
using var cmd = new NpgsqlCommand();
cmd.Connection = con;
var result = GetBySubject();
foreach (var item in result)
{
    Console.WriteLine($"{item.Id} {item.FirstName}");
}

IEnumerable<Person> GetBySubject()
{
    cmd.CommandText = $"SELECT * FROM persons";
    NpgsqlDataReader reader = cmd.ExecuteReader();
    var result = new List<Person>();
    while (reader.Read())
    {
        result.Add(new Person()
        {
            Id = reader["Id"],
            FirstName = reader[1] as string,
            LastName = reader[2] as string,
        });
    }
    return result;
}

class Person
{
    public object Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}