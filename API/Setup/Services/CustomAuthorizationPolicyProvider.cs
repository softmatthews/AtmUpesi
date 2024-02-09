using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Persistence;

namespace API.Setup.Services
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly DefaultAuthorizationPolicyProvider _fallbackProvider;
        private readonly IServiceScopeFactory _scopeFactory;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _fallbackProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _fallbackProvider.GetDefaultPolicyAsync()!;

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _fallbackProvider.GetFallbackPolicyAsync()!;

        public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var claim = context.Right.FirstOrDefault(x => x.Name == policyName);
            if (claim == null)
            {
                var fallbackPolicy = await _fallbackProvider.GetPolicyAsync(policyName) ?? throw new InvalidOperationException($"No policy found: {policyName}");
                return fallbackPolicy;
            }

            var authorizationPolicy = new AuthorizationPolicyBuilder();
            var policy = authorizationPolicy
                            .RequireAuthenticatedUser()
                            .RequireClaim(Enum.GetName(typeof(ERights), claim.Type)!, claim.Name); //  claim.Feature
            var policyBuild = policy.Build();
            return await Task.FromResult(policyBuild);
        }

    }
}
