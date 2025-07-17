using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DataTransferObjects.ViewModels
{
	public class ResponseObject<T>
	{
		public int Status { get; set; }
		public string Message { get; set; }
		public T? Data { get; set; }
		public ResponseObject()
		{

		}
		public ResponseObject(int status, string message, T? data = default)
		{
			Status = status;
			Message = message;
			Data = data;
		}
	}
}
