namespace Cinema.Model
{
    public class Director
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Film> Films { get; set; }
    }
}
