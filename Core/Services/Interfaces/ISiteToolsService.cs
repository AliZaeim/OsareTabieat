using DataLayer.Entities.Supplementary;

namespace Core.Services.Interfaces
{
    public interface ISiteToolsService
    {
        #region Generic
        Task<SiteInfo> GetLastSiteInfoAsync();
        void SaveChanges();
        Task SaveChangesAsync();
        #endregion
        #region SliderBanner
        Task<List<Slider>> GetSlidersAsync();
        Task<List<BannerNextSlider>> GetBannerNextSlidersAsync();
        #endregion
        #region State
        Task<List<State>> GetStatesAsync();
        Task<State> GetStateAsync(int Id);
        void CreateState(State state);
        void UpdateState(State state);
        void CreateStateFreight(StateFreight stateFreight);
        void EditStateFreight(StateFreight stateFreight);
        Task DeleteStateFreight(int stfId);
        Task<List<StateFreight>> GetStateFreightAsync();
        Task<StateFreight> GetStateFreightAsync(int Id);
        #endregion
        #region Terms
        Task<Term> GetLastTermAsync();
        #endregion
    }
}
