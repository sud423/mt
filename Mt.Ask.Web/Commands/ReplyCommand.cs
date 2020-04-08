using MediatR;

namespace Mt.Ask.Web.Commands
{
    public class ReplyCommand : INotification
    {
        public int UserId { get; set; }

        public string ToUser { get; set; }
        
        public string Url { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        public int ReplyId { get; set; }

    }
}
