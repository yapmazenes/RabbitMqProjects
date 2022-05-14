using MessageConsumer.Services.Models;
using System.Threading.Tasks;

namespace MessageConsumer.Services.Abstract
{
    public interface IEmailService
    {
        Task<bool> EmailSend(EmailModel emailModel);
    }
}
