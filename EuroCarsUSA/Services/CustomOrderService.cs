using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models.Form;
using EuroCarsUSA.Resources;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;

namespace EuroCarsUSA.Services
{
    public class CustomOrderService : ICustomOrderService
    {
        private readonly ICustomOrderRepository _customOrderRepository;
        private readonly IEmailService _emailService;
        private readonly Localizer _localizer;

        public CustomOrderService(ICustomOrderRepository customorderRepository, IEmailService emailService, Localizer localizer)
        {
            _customOrderRepository = customorderRepository;
            _emailService = emailService;
            _localizer = localizer;
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
                Status = co.Status,
                TimeStamp = co.TimeStamp
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
                if (!string.IsNullOrEmpty(customOrderViewModel.Email))
                {
                    _emailService.SendEmail(customOrderViewModel.Email, _localizer.CustomOrderEmailSubject, _localizer.CustomOrderEmailBody);
                    _emailService.SendEmail(_emailService.AdminEmail, _localizer.AdminCustomOrderEmailSubject, string.Format(_localizer.AdminCustomOrderEmailBody, customOrderViewModel.PhoneNumber, customOrderViewModel.Email));
                }
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
