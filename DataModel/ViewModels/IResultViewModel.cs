namespace DataModel.ViewModels
{
    public interface IResultViewModel<TEntity>
    {
        ErrorModel Error { get; set; }
        TEntity Result { get; set; }
        ResultStatus ResultStatus { get; set; }
    }
}