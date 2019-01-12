using System.Threading.Tasks;
using SetareSazBot.API.Json.Input;
using SetareSazBot.API.Json.Output;

namespace SetareSazBot.Service.Interface
{
    public interface IMessengerService
    {
        Task<SendMessagesOutput> SendMessage(SendMessageInput model);
        Task<GetMessageOutput> GetMessages(long lastMessageId);
        Task<PaymentRequestOutput> PaymentRequest(PaymentRequestInput paymentRequestInput);

        Task<SettlePaymentOutput> SettlePayment(string chatId, string orderId, string paymentToken);
    }
}