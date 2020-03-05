using Microsoft.AspNetCore.Http;
using System;

namespace DetranConsulta
{
    public interface IRenachStorage
    {
        string BuscarRenach();
        void DefinirRenach(string renach);
    }

    public class RenachStorage : IRenachStorage
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private HttpContext Context => this.httpContextAccessor.HttpContext;

        public RenachStorage(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string BuscarRenach()
        {
            if (this.Context.Request.Cookies.TryGetValue("renach", out var renach))
                return renach;

            return null;
        }

        public void DefinirRenach(string renach)
        {
            this.Context.Response.Cookies.Append("renach", renach, new CookieOptions
            {
                Expires = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0))
            });
        }
    }
}