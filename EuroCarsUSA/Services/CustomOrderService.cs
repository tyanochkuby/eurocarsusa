using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models.Form;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services
{
    public class CustomOrderService : ICustomOrderService
    {
        private readonly ICustomOrderRepository _customOrderRepository;
        private readonly IEmailService _emailService;

        public CustomOrderService(ICustomOrderRepository customorderRepository, IEmailService emailService)
        {
            _customOrderRepository = customorderRepository;
            _emailService = emailService;
        }

        public async Task<List<CustomOrderViewModel>> GetAll()
        {
            var customOrders = await _customOrderRepository.GetAll();
            
            var viewModels = customOrders.OrderByDescending(c => c.TimeStamp).Select(co => new CustomOrderViewModel()
            {
                Id = co.Id,
                Forms = co.Forms.Select(f => f.ToViewModel()).ToList(),
                Email = co.Email,
                Name = co.Name,
                PhoneNumber = co.PhoneNumber,
                Status = co.Status
            }).ToList();

            return viewModels;
        }

        public async Task<CustomOrderViewModel> GetById(Guid id)
        {
            var customOrder = await _customOrderRepository.GetById(id);
            if (customOrder == null)
            {
                return null;
            }

            var viewModel = new CustomOrderViewModel
            {
                Id = customOrder.Id,
                Forms = customOrder.Forms.Select(f => f.ToViewModel()).ToList(),
                Email = customOrder.Email,
                Name = customOrder.Name,
                PhoneNumber = customOrder.PhoneNumber,
                Status = customOrder.Status
            };

            return viewModel;
        }

        public async Task<Guid?> SubmitOrderAsync(CustomOrderViewModel customOrderViewModel)
        {
            var orderId = customOrderViewModel.Id == Guid.Empty ? Guid.NewGuid() : customOrderViewModel.Id;

            var forms = customOrderViewModel.Forms.Select(form => form.ToForm()).ToList();

            var customOrder = new CustomOrder
            {
                Id = orderId,
                Forms = forms,
                Status = OrderStatus.Sent,
                Email = customOrderViewModel.Email,
                PhoneNumber = customOrderViewModel.PhoneNumber,
                Name = customOrderViewModel.Name
            };

            if (await _customOrderRepository.Add(customOrder))
            {
                return customOrder.Id;
            }

            return null;
        }

        public async Task<bool> UpdateStatus(Guid id, OrderStatus status)
        {
            var customOrder = await _customOrderRepository.GetById(id);
            if (customOrder == null)
            {
                return false;
            }
            customOrder.Status = status;
            return await _customOrderRepository.Update(customOrder);
        }
    }
}
