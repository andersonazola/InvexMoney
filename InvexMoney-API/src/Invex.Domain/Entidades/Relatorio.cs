namespace Invex.Domain.Entidades;

public class Relatorio
{
    public int Id { get; set; }
    public DateTime DataReferencia { get; set; }
    public DateTime GeradoEm { get; set; }
    public string Conteudo { get; set; }


    public Relatorio() { }

    public Relatorio(DateTime dataReferencia, string conteudo)
    {
        if (dataReferencia == default || dataReferencia > DateTime.UtcNow)
            throw new ArgumentException("Data de referência inválida");

        if (string.IsNullOrWhiteSpace(Conteudo))
            throw new ArgumentException("O conteúdo do relatório nãoi pode estar vazio");

        DataReferencia = dataReferencia;
        Conteudo = conteudo;
        GeradoEm = DateTime.UtcNow;
    }


}