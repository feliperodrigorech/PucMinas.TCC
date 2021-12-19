using System;

namespace PucMinas.TCC.Domain.Models
{
    public class ReturnModel<T>
    {
        public bool IsError { get; set; }
        public string MessageError { get; set; }
        public T Object { get; set; }

        public void SetError(Exception ex)
        {
            IsError = true;
            MessageError = ex.Message;
            Object = default;
        }
    }
}
