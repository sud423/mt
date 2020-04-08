using MediatR;

namespace Mt.Ask.Web.Commands
{
    public class AgreeCommand : INotification
    {
        public string ToUser { get; set; }

        public int ReplyId { get; set; }

        public int UserId { get; set; }

        public string Topic { get; set; }

        public string NickName { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }
    }
}
