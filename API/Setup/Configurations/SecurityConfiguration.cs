namespace API.Setup.Configurations
{
    internal static class SecurityConfiguration
    {
        public static void Configure(IApplicationBuilder app)
        {
   //			app.UseXContentTypeOptions();
			//app.UseReferrerPolicy(opt => opt.NoReferrer());
			//app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
			//app.UseXfo(opt => opt.Deny());
			// app.UseCspReportOnly(opt => opt
			// 		.BlockAllMixedContent()
			// 		.StyleSources(s => s.Self())
			// 		.FontSources(s => s.Self())
			// 		.FormActions(s => s.Self())
			// 		.FrameAncestors(s => s.Self())
			// 		.ImageSources(s => s.Self())
			// 		.ScriptSources(s => s.Self())
			// 		.ReportUris(r => r.Uris("/reports"))
			// );

			// app.UseHttpsRedirection();

			app.UseCors("CorsPolicy");
        }
    }
}
