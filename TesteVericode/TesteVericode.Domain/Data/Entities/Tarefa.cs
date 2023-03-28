namespace TesteVericode.Domain.Entities
{
    public class Tarefa : BaseEntity
    {
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
    }
}
