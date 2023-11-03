using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Supplementary;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{

    public class SiteToolsService : ISiteToolsService
    {
        private readonly MyContext _context;
        public SiteToolsService(MyContext context)
        {
            _context = context;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #region Generic
        public async Task<SiteInfo> GetLastSiteInfoAsync()
        {
            List<SiteInfo> siteInfos = await _context.SiteInfos.OrderByDescending(x => x.RegDate).ToListAsync();
            return siteInfos.FirstOrDefault();

        }
        #endregion
        #region SliderBanner
        public async Task<List<BannerNextSlider>> GetBannerNextSlidersAsync()
        {
            return await _context.BannerNextSliders.Include(x => x.BannerNextBase).ToListAsync();
        }



        public async Task<List<Slider>> GetSlidersAsync()
        {
            return await _context.Sliders.ToListAsync();
        }


        #endregion
        #region State
        public async Task<List<State>> GetStatesAsync()
        {
            return await _context.States.Include(z => z.Counties).Include(y => y.StateFreights).ToListAsync();
        }

        public async Task<State> GetStateAsync(int Id)
        {
            return await _context.States.Include(x => x.Counties).Include(x => x.StateFreights).SingleOrDefaultAsync(x => x.StateId == Id);
        }

        public void CreateState(State state)
        {
            _context.States.Add(state);
        }

        public void UpdateState(State state)
        {
            _context.States.Update(state);
        }

        public void CreateStateFreight(StateFreight stateFreight)
        {
            _context.StateFreights.Add(stateFreight);
        }

        public void EditStateFreight(StateFreight stateFreight)
        {
            _context.StateFreights.Update(stateFreight);
        }

        public async Task DeleteStateFreight(int stfId)
        {
            StateFreight? stateFreight = await _context.StateFreights.FindAsync(stfId);
            if (stateFreight != null)
            {
                _context.StateFreights.Remove(stateFreight);
            }
        }

        public async Task<List<StateFreight>> GetStateFreightAsync()
        {
            return await _context.StateFreights.Include(x => x.State).ToListAsync();
        }

        public async Task<StateFreight> GetStateFreightAsync(int Id)
        {
            return await _context.StateFreights.Include(x => x.State).SingleOrDefaultAsync(x => x.Id == Id);
        }
        #endregion
        #region Term
        public async Task<Term> GetLastTermAsync()
        {
            return await _context.Terms.OrderByDescending(x => x.RegDate).FirstOrDefaultAsync(); 
        }
        #endregion Term
    }
}
