namespace GestaoDocumental.Shared.Security;

public static class AppPolicies
{
    public const string AdministradorOnly = "AdministradorOnly";
    public const string PodeGerirUsuarios = "PodeGerirUsuarios";
    public const string PodeGerirDocumentos = "PodeGerirDocumentos";
    public const string PodeConsultarDocumentos = "PodeConsultarDocumentos";
    public const string PodeAprovarDocumentos = "PodeAprovarDocumentos";
}
