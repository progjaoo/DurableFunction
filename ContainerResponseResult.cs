using System.Collections.Generic;

public class ContainerResponseResult
{
    public List<string> NomeDocumentos { get; set; }

    public ContainerResponseResult()
    {
        NomeDocumentos = new List<string>();
    }
}