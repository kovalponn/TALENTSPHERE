using Microsoft.AspNetCore.Html;

namespace TALENTSPHERE.Helpers
{
    public static class ChatHelpers
    {
        public static IHtmlContent GenerateMessage(string body, string recipientLogin, string senderLogin, string dateTime)
        {
            var builder = new HtmlContentBuilder();
            if (recipientLogin == senderLogin)
            {
                builder.AppendHtml($"<div id=\"myMessageExample\" class=\"d-flex justify-content-start my-4\">\r\n        <div class=\"card bg-primary text-white\" style=\"max-width: 75%;\">\r\n            <div class=\"card-body p-2\">\r\n                <p id=\"txtBody\" class=\"mb-1\">{body}</p>\r\n            </div>\r\n            <div class=\"card-footer bg-primary text-end small\">\r\n                <span id=\"nameBody\">{recipientLogin}</span> | <span id=\"time\">{dateTime}</span>\r\n            </div>\r\n        </div>\r\n    </div>");
                return builder;
            }
            else
            {
                builder.AppendHtml($"<div id=\"otherMessageExample\" class=\"d-flex justify-content-start my-4\">\r\n        <div class=\"card bg-secondary text-white\" style=\"max-width: 75%;\">\r\n            <div class=\"card-body p-2\">\r\n                <p id=\"txtBody\" class=\"mb-1\">{body}</p>\r\n            </div>\r\n            <div class=\"card-footer bg-secondary text-start small\">\r\n                <span id=\"nameBody\">{recipientLogin}</span> | <span id=\"time\">{dateTime}</span>\r\n            </div>\r\n        </div>\r\n    </div>");
                return builder;
            }

        }

        public static IHtmlContent GenerateChatIcon(string chatId, string chatName)
        {
            var builder = new HtmlContentBuilder();
            builder.AppendHtml($"<div class=\"chat-item\"> \r\n            <a href=\"Chat?chatId={chatId}\">{chatName}</a>\r\n        </div>");
            return builder;
        }
    }
}
