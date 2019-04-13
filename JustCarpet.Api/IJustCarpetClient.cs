using JustCarpet.Api.Models;
using JustCarpet.Api.Models.Flooring;
using JustCarpet.Api.Models.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustCarpet.Api
{
    public interface IJustCarpetClient
    {
        Task<bool> AddOrderReview(Review model);
        Task<Flooring> GetFlooringDetails(int id);
        Task<List<Appointment>> GetInstallerAppontments(int id);
        Task<List<Installer>> GetInstallers();
        Task<OrderConfirmation> Neworder(CreateOrderDto model);
        Task<Customer> Register(string macAddress);
        Task<List<Flooring>> Search(Search search);
        Task<bool> UpdateCustomer(Customer model);
    }
}