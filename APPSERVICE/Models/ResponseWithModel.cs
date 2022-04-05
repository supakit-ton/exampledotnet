using System.Text.Json;

namespace APPSERVICE.Models
{
    public class ResponseWithModel<T> where T : class
    {
        public string result { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<ResponseWithModel<T>>(this);
        }
    }
}
