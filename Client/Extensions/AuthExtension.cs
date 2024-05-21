using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using GreenCoinHealth.Shared;

namespace GreenCoinHealth.Client.Extensions
{
    public class AuthExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthExtension(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task ActualizarEstadoAutenticacion(SessionDTO? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                var claims = new List<Claim>();

                if (!string.IsNullOrEmpty(sesionUsuario.Nombre))
                {
                    claims.Add(new Claim(ClaimTypes.Name, sesionUsuario.Nombre));
                }

                if (!string.IsNullOrEmpty(sesionUsuario.Correo))
                {
                    claims.Add(new Claim(ClaimTypes.Email, sesionUsuario.Correo));
                }

                if (!string.IsNullOrEmpty(sesionUsuario.Rol))
                {
                    claims.Add(new Claim(ClaimTypes.Role, sesionUsuario.Rol));
                }

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));
                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<SessionDTO>("sesionUsuario");

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(sesionUsuario.Nombre))
            {
                claims.Add(new Claim(ClaimTypes.Name, sesionUsuario.Nombre));
            }

            if (!string.IsNullOrEmpty(sesionUsuario.Correo))
            {
                claims.Add(new Claim(ClaimTypes.Email, sesionUsuario.Correo));
            }

            if (!string.IsNullOrEmpty(sesionUsuario.Rol))
            {
                claims.Add(new Claim(ClaimTypes.Role, sesionUsuario.Rol));
            }

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }
    }
}
