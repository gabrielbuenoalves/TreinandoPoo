using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace web
{
    public interface IRelatorio
    {
        Task imprimir(HttpContext context);
    }
}