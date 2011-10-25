using System.Windows.Input;
using HibernatingRhinos.Orders.Backend.Commands;
using HibernatingRhinos.Orders.Backend.Features.Products;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public abstract class PageableModel : ModelBase
    {
        public PagingInfo Paging { get; set; }

        public const int ItemsPerPage = 20;

        public class PagingInfo
        {
            public PagingInfo()
            {
                NumberOfPages = new Observable<int>();
                NumberOfItems = new Observable<int>();

            }

            public int PageNumber { get; set; }
            public Observable<int> NumberOfPages { get; set; }
            public Observable<int> NumberOfItems { get; set; }
        }

        public PageableModel()
        {
            int pageNumber;
            int.TryParse(GetQueryParam("pageNumber"), out pageNumber);

            Paging = new PagingInfo
            {
                PageNumber = pageNumber
            };

            Paging.NumberOfPages.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged("NextPage");
                OnPropertyChanged("Paging");
            };
            Paging.NumberOfItems.PropertyChanged += (sender, args) =>
            {
                OnPropertyChanged("PageDataConverter");
                OnPropertyChanged("Paging");
            };
        }

        public ICommand PreviousPage { get { return new UpdateUrlCommand(CreateUrl(Paging.PageNumber - 1), Paging.PageNumber > 0); } }
        public ICommand NextPage { get { return new UpdateUrlCommand(CreateUrl(Paging.PageNumber + 1), Paging.PageNumber < Paging.NumberOfPages.Value); } }
        public ICommand SearchUrl { get { return new UpdateUrlCommand(CreateUrl(0), true); } }



        protected abstract string CreateUrl(int pageNumber);

    }
}
