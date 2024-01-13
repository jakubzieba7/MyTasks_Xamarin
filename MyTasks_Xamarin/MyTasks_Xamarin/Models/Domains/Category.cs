using SQLite;

namespace MyTasks_Xamarin.Models.Domains
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
