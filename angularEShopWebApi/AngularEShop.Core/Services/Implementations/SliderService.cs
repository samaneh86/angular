using AngularEShop.Core.Services.Interfaces;
using AngularEShop.DataLayer.Entities.Site;
using AngularEShop.DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace AngularEShop.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        private IGenericRepository<Slider> sliderRepository;
        public SliderService(IGenericRepository<Slider> _sliderRepository)
        {
            sliderRepository = _sliderRepository;
        }
        public async Task<List<Slider>> GetAllSliders()
        {
            return await sliderRepository.GetEntitiesQuery().ToListAsync();
        }
        public async Task<List<Slider>> GetActiveSliders()
        {
            return await sliderRepository.GetEntitiesQuery().Where(x => !x.IsDelete).ToListAsync();
        }
        public async Task AddSlider(Slider slider)
        {
            await sliderRepository.AddEntity(slider);
            await sliderRepository.SaveChanges();
        }
        public async Task UpdateSlider(Slider slider)
        {
             sliderRepository.UpdateEntity(slider);
            await sliderRepository.SaveChanges();
        }
        public async Task<Slider> GetSliderById(long sliderId)
        {
           return await sliderRepository.GetEntityById(sliderId);
        }
        public void Dispose()
        {
            sliderRepository?. Dispose();
        }

     

      

     

        
    }
}
