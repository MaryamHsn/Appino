using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ViewModels
{
    public partial class ResultViewModel<TEntity> : IResultViewModel<TEntity>
    {
        public ResultStatus ResultStatus { get; set; }
        public virtual ErrorModel Error { get; set; }
        public TEntity Result { get; set; }
        public ResultViewModel()
        {
            Error = new ErrorModel();
        }
    }
}
