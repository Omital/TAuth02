using Abp.Web.Mvc.Views;

namespace TAuth02.Web.Views
{
    public abstract class TAuth02WebViewPageBase : TAuth02WebViewPageBase<dynamic>
    {

    }

    public abstract class TAuth02WebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected TAuth02WebViewPageBase()
        {
            LocalizationSourceName = TAuth02Consts.LocalizationSourceName;
        }
    }
}