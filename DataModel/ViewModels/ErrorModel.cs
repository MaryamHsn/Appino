using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ViewModels
{
    public class ErrorModel 
    {
        public virtual ErrorModels ErrorModels { get; set; }

        public string Messsage { get; set; }
        private Exception _dummyException;
        public Exception Exception
        {
            get { return _dummyException; }
            set
            {
                this.ErrorModels.AddExceptionAndDetail(value);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return ErrorModels.ToString();
            }
            set
            {
                this.ErrorModels.Add(value);
            }
        }
        public ErrorModel()
        {
            //_dummyException = new Exception();
            this.ErrorModels = new ErrorModels();
        }
    }

    public class ErrorModels : List<string>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item + "\r\n");
            }
            return sb.ToString();
        }
        public void AddExceptionAndDetail(Exception ex)

        {
            while (ex != null)
            {
                this.Add(ex.Message);
                ex = ex.InnerException;
            }
        }
    }



}
