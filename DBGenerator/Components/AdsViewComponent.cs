using DBGenerator.Data;
using DBGenerator.Models.Ads;
using Microsoft.AspNetCore.Mvc;

namespace DBGenerator.Components
{
    public class AdsViewComponent : ViewComponent
    {
        private IDataFacade _data;

        public AdsViewComponent(IDataFacade data)
        {
            _data = data;
        }

        public async Task<IViewComponentResult> InvokeAsync(Position position)
        {
            var model = new AdsViewModel();
            model.Ads = await _data.GetAds(position, true, true);
            model.Position = position;

            return View(model);
        }
    }
}
