using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Business.Abstract;
using Core.Utilities.Security.JWT;
using System.Security.Claims;

namespace Business.BusinessAspects.Autofac.Authentication
{
    public class AuthenticationAttribute : MethodInterception
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IAuthService _authService;
        private ITokenHelper _tokenHelper;
        public AuthenticationAttribute()
        {
            Priority = 1;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _authService = (IAuthService)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(IAuthService));
            _tokenHelper = (ITokenHelper)_httpContextAccessor.HttpContext.RequestServices.GetService(typeof(ITokenHelper));
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var isAuthenticated = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
            if (!isAuthenticated) throw new UnauthorizedException();
        }
    }
}
