namespace TesteVericode.Domain.DTO
{
    public class CreateTarefaDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
    }
}
